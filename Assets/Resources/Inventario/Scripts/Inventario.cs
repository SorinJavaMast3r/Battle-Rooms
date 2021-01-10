using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Inventario : MonoBehaviour
{
    /*Las modificaciones que haga en el inventario se van a ver reflejadas en los slots*/

    #region Propiedades
    [SerializeField]
    private Database database; //Referencia a la base de datos

    [SerializeField]
    private GameObject slotPrefab; //Referencia al prefab del slot

    [SerializeField]
    private Transform PanelInventario; //Referencia al panel de inventario

    [SerializeField]
    private List<InformacionSlot> listaInformacionSlot; //Lista con la informacion de todos los slots (inventario propiamente dicho)

    [SerializeField]
    private int capacidad; //Capacidad del inventario

    private string jsonString; //Texto en formato json que usaremos para guardar el inventario

    private bool inventarioActivo;
    public Slot miSlot;

    public static Inventario llamar;
    #endregion

    #region Inicio y Update
    public void Awake()
    {
        llamar = this;
    }
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        listaInformacionSlot = new List<InformacionSlot>();

        //Si en playerPrefs existe la clave inventario, el jugador ya tiene un inventario guardado en el juego por lo que podra recuperarlo
        if (PlayerPrefs.HasKey("inventario"))
            CargarInventarioGuardado(); //recupera inventario guardado        
        else
            CargarInventarioVacio();    //Crea uno nuevo        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventarioActivo = !inventarioActivo;
        }
        if (inventarioActivo)
        {
            PanelInventario.gameObject.SetActive(true);
        }
        else
        {
            PanelInventario.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Cargar inventario
    private void CargarInventarioVacio()
    {
        //Me recorre cada uno de los slot del inventario.
        for (int i = 0; i < capacidad; i++)
        {
            //Vamos a crear el objeto que va a tener que llevar esto en la escena
            GameObject slot = Instantiate<GameObject>(slotPrefab, PanelInventario);//le pasa como padre este transform
            Slot nSlot = slot.GetComponent<Slot>();//Extraemos el codigo del slot. Con esto podemos utilizar las cosas de la clase slot
            nSlot.SetUp(i);//Acccedemos al SetUp del slot y le pasamos la i como id
            nSlot.database = database;//Asignamos a cada slot la base de datos
            InformacionSlot ninformacionSlot = nSlot.informacionSlot; // creamos un objeto de la clase informacionSlot con el SetUp hecho 
            listaInformacionSlot.Add(ninformacionSlot);//Añadimos el slot al inventario
        }
    }

    private void CargarInventarioGuardado()
    {
        jsonString = PlayerPrefs.GetString("inventario");//Nos devuelve la cadena almacenada en PlayerPrefs y la va a poner en jsonString
        InventoryWrapper inventoryWrapper = JsonUtility.FromJson<InventoryWrapper>(jsonString);//Como la cadena jsonString esta en formato json la transformamos en un objeto de tipo InventoryWrapper
        this.listaInformacionSlot = inventoryWrapper.listaInformacionSlots;
        for (int i = 0; i < capacidad; i++)
        {

            GameObject slot = Instantiate<GameObject>(slotPrefab, PanelInventario);
            Slot nSlot = slot.GetComponent<Slot>();
            nSlot.SetUp(i);
            nSlot.database = database;
            nSlot.informacionSlot = listaInformacionSlot[i];
            nSlot.UpdateUI();
        }
    }
    #endregion

    #region Buscar slot e item
    private InformacionSlot BuscarItemEnInventario(int idItem)
    {
        foreach (InformacionSlot informacionSlot in listaInformacionSlot)
        {
            if (informacionSlot.idItem == idItem && informacionSlot.vacio == false)
                return informacionSlot;
        }
        return null;
    }

    private InformacionSlot buscarSlotAdecuado(int idItem)
    {
        //Va a ver si el item existe en nuestro inventario,
        //Si existe, devolvemos el slot donde se encuentra siempre que no haya llegado a su capacidad maxima
        //Si no existe ese item devolvemos un slot vacio
        foreach (InformacionSlot informacionSlot in listaInformacionSlot)
        {
            if (informacionSlot.idItem == idItem && informacionSlot.cantidad < informacionSlot.cantidadMaxima && !informacionSlot.vacio && database.FindItemInDatabase(idItem).estaqueable)
                return informacionSlot;

        }
        foreach (InformacionSlot informacionSlot in listaInformacionSlot)
        {
            if (informacionSlot.vacio)
            {
                informacionSlot.SlotVacio();
                return informacionSlot;
            }
        }

        return null;
    }

    private Slot BuscarSlot(int id)
    {
        return PanelInventario.GetChild(id).GetComponent<Slot>(); //Obtenemos el elemento slot
    }
    #endregion

    #region Funciones añadir, borrar y usar
    public void AddItem(int idItem)
    {
        Item item = database.FindItemInDatabase(idItem);//Busca en la base de datos

        if (item != null)//Si el item existe en la base de datos hace lo siguiente
        {
            InformacionSlot informacionSlot = buscarSlotAdecuado(idItem); //Encontrar en donde guardar el item
            Debug.Log(informacionSlot);
            if (informacionSlot != null)//Si el item ya esta guardado lo que hace es
            {
                informacionSlot.cantidad++; //Aumenta la cantidad del itme
                informacionSlot.idItem = idItem;//Hago que el item sea igual
                informacionSlot.vacio = false; //Pongo el slot como que no esta vacio

                BuscarSlot(informacionSlot.id).UpdateUI();//El slot que tenga el item llama al metodo UpdateUI que actuliza la imagen y el texto con la cantidad
            }
        }
    }

    public void RemoveItem(int idItem)
    {
        InformacionSlot informacionSlot = BuscarItemEnInventario(idItem);

        if (informacionSlot != null)
        {
            if (informacionSlot.cantidad == 1)
                informacionSlot.SlotVacio();
            else
                informacionSlot.cantidad--;
        }

        BuscarSlot(informacionSlot.id).UpdateUI();
    }

    public void RemoveItem(int idItem, InformacionSlot informacionSlot)
    {
        if (informacionSlot != null)
        {
            if (informacionSlot.cantidad == 1)
                informacionSlot.SlotVacio();
            else
                informacionSlot.cantidad--;
        }

        BuscarSlot(informacionSlot.id).UpdateUI();
    }

    public void UsarItem(int idItem, InformacionSlot informacionSlot)
    {
        Item item = database.FindItemInDatabase(idItem);
        InteraccionItems interaccion = new InteraccionItems();
        if (item != null)//Si el item existe en la base de datos hace lo siguiente
        {
            if (item.tipo == Item.TipoDeItem.consumible)
            {
                switch (idItem)
                {
                    case 1: interaccion.PocionVida(); break;
                    case 2: interaccion.PocionMana(); break;
                }
            }
            if (item.tipo == Item.TipoDeItem.otro)
            {
                //Futura actualizacion
            }
        }
        RemoveItem(idItem, informacionSlot);
    }
    #endregion


    public void GuardarInventario()
    {
        InventoryWrapper inventoryWrapper = new InventoryWrapper();
        inventoryWrapper.listaInformacionSlots = this.listaInformacionSlot;
        jsonString = JsonUtility.ToJson(inventoryWrapper);//Toma toda la informacion que esta guardando en la lista y la serializa en formato json y la almacena jsonString
        PlayerPrefs.SetString("inventario", jsonString);//Si no existe la clave inventario la va a crear y la va a asignar la cadena jsonString, y si existe ps sobreescribe el jsonString
    }

    public void IntercambiarSlot(int id_origen, int id_destino, Transform imagen_origen, Transform imagen_destino)
    {
        //Intercambiar imagen
        imagen_origen.SetParent(PanelInventario.GetChild(id_destino)); //Pasa la imagen de un slot a otro
        imagen_destino.SetParent(PanelInventario.GetChild(id_origen));
        imagen_origen.localPosition = Vector3.zero; //Centramos la imagen
        imagen_destino.localPosition = Vector3.zero;

        if (id_origen != id_destino)
        {
            InformacionSlot informacionOrigen = listaInformacionSlot[id_origen];//Cogo la infomacion del slot correspontiente (slot origen)
            InformacionSlot informacionDestino = listaInformacionSlot[id_destino];//Cogo la infomacion del slot correspontiente (slot destino)

            //Intercambio en invetario
            listaInformacionSlot[id_origen] = informacionDestino;//Pasa la informacion del slot de destino al de origen (Tanto la informacion del slot como la del item)
            listaInformacionSlot[id_origen].id = id_origen; //Como no quiero que se intercambien los id del slot pues le asigno de nuevo el que tenia
            listaInformacionSlot[id_destino] = informacionOrigen;//Pasa la informacion del slot de origen al de destino
            listaInformacionSlot[id_destino].id = id_destino;//Como no quiero que se intercambien los id del slot pues le asigno de nuevo el que tenia

            //Intercambio en los slots basados en los cambios del inventario
            Slot slotOrigen = PanelInventario.GetChild(id_origen).GetComponent<Slot>();
            slotOrigen.informacionSlot = listaInformacionSlot[id_origen];//La informacion que tengo guardada en la lista del inventario se la asigno al slot
            Slot slotDestino = PanelInventario.GetChild(id_destino).GetComponent<Slot>();
            slotDestino.informacionSlot = listaInformacionSlot[id_destino];

            slotOrigen.imagenItem = imagen_destino.GetComponent<Image>();//Coge la imagen del item destino y se la aplica al de origen
            slotDestino.imagenItem = imagen_origen.GetComponent<Image>();//Coge la imagen del item origen y se la aplica al de destino

            slotOrigen.textoCantidad = slotOrigen.imagenItem.transform.GetChild(0).GetComponent<Text>();//Le asignamos el texo de origen ya que lo hemos intercambiado antes en las imagenes
            slotDestino.textoCantidad = slotDestino.imagenItem.transform.GetChild(0).GetComponent<Text>();
        }
    }

    private class InventoryWrapper
    {
        public List<InformacionSlot> listaInformacionSlots;
    }

    #region Añadir items
    void OnTriggerEnter(Collider obj)
    {
        switch (obj.tag)
        {
            case "Pocion_vida":
                AddItem(1);
                Destroy(obj.gameObject);
                break;
            case "Pocion_mana":
                AddItem(2);
                Destroy(obj.gameObject);
                break;
        }
    }
    #endregion

    #region Pruebas
    [ContextMenu("Test Add - idItem = 1")]
    public void TestAdd()
    {
        AddItem(1);
    }
    [ContextMenu("Test Add - idItem = 2")]
    public void TestAdd2()
    {
        AddItem(2);
    }
    [ContextMenu("Test Remove - idItem = 1")]
    public void TestRemove()
    {
        RemoveItem(1);
    }
    [ContextMenu("Test Save")]
    public void TestSave()
    {
        GuardarInventario();
    }
    #endregion

}
