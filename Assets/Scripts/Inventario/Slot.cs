using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler //Esta interfaz declara a esta clase como una en la que se puede soltar un item
{
    public Database database;
    public Image imagenItem;
    public Text textoCantidad;
    public InformacionSlot informacionSlot;

    public void OnDrop(PointerEventData eventData)
    {
        TirarItem tirar = new TirarItem();
        tirar = eventData.pointerDrag.GetComponent<TirarItem>();//pointerDrag me devuelve el objeto que esta recibiendo la funcion onDrag del otro codigo. Por lo qyue vamos a obtener la imagen que estamos arrastrando
        tirar.SlotDestino = this; //Aqui le digo que cuando suelte la imagen va a tener como destino el slot sobre el que la solte
    }

    public void SetUp(int id)
    {
        informacionSlot = new InformacionSlot();
        informacionSlot.id = id;
        informacionSlot.SlotVacio();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("Id item" + this.informacionSlot.idItem);
        Inventario.llamar.UsarItem(this.informacionSlot.idItem, this.informacionSlot);        
    }

    //Metodo que nos permite actualizar la cantidad y la imagen de un item que se contiene en un slot
    public void UpdateUI()
    {
        if (informacionSlot.vacio)
        {
            imagenItem.sprite = null;
            imagenItem.enabled = false;             
        }
        else
        {
            imagenItem.sprite = database.FindItemInDatabase(informacionSlot.idItem).imagenItem;//Vamos a buscar el item en la base de datos y extraemos su imagen
            imagenItem.enabled = true;
            imagenItem.gameObject.SetActive(true);
            if (informacionSlot.cantidad > 1)
            {
                textoCantidad.text = informacionSlot.cantidad.ToString();
                textoCantidad.gameObject.SetActive(true);
            }
            else
            {
                textoCantidad.gameObject.SetActive(false);
            }
        }
    }
}

[System.Serializable]
public class InformacionSlot
{
    public int id;
    public bool vacio;
    public int idItem;
    public int cantidad;
    public int cantidadMaxima = 10;

    public void SlotVacio()
    {
        vacio = true;
        cantidad = 0;
        idItem = -1;
    }
}
