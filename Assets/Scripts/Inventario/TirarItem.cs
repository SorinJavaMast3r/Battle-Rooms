using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TirarItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //La i hace referencia a las interfaces
{
    public Inventario inventario;
    public Transform panelInventario;
    public Slot miSlot;//Es el slot del que viene el objeto, es decir , donde esta la imagen
    public Slot SlotDestino;//Es el slot de destino
    public Image imagen;//Imagen del item

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();//Busca en la geraarquia hasta el primer objeto que tenga un componente del tipo inventario
        panelInventario = transform.parent.parent;//Obtenemos el panelInventrio diciendole la ruta de padres partiendo del objeto que contiene este script
        imagen = this.GetComponent<Image>();//
    }

    //Es um evento que se dispara cuando inico el drag de un objeto
    public void OnBeginDrag(PointerEventData eventData)
    {
        miSlot = transform.parent.GetComponent<Slot>();//<obtengo la refeerncia al slot. Pongo dentro de getComponent slot porque mi padre tiene que ser si o si un slot
        transform.SetParent(panelInventario);//<Cambio el padre, por lo que ahora la imagen pasaria a ser hija del panelInventario y no de slot;
        transform.position = eventData.position; //Obtiene la posicion del click del mouse. (Con estyo voy a cambiar la imagen hasta donde esta el puntero del mouse)
        imagen.raycastTarget = false;//Significa que una vez yo ya hice click a la imagen no le puedo volver a hacer click de nuevo hasta que la suelte. Esto nos permitira mas adelante dejar la imagen en otro slot
    }
    //Este se ejecuta constantemente mientras hago el drag
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;//Que se vaya actualizando la posicion del mouse
    }
    //Este se ejecuta cuando termino el drag
    public void OnEndDrag(PointerEventData eventData)
    {
        if (SlotDestino != null)//Como el destino solo puede ser un slot, pues le decimos que si es diferente de nulo(Lo hemos arrastrado hasta el slotdetino)
        {
            if (SlotDestino.informacionSlot.id != miSlot.informacionSlot.id)//Si el slot destino es diferente que el slot de donde he cogido la imagen
            {
                inventario.IntercambiarSlot(miSlot.informacionSlot.id, SlotDestino.informacionSlot.id, this.transform, SlotDestino.imagenItem.transform); //this.transform es la imagen. Esta funcio intercambia los datos entre un slot y otro (Si movemos un item ed un sot a otro,vacia el slot y pasa el item al otro slot)
                SlotDestino.imagenItem.transform.localPosition = Vector3.zero;//Voy a centrar la imagen
            }
            else
            {
                inventario.IntercambiarSlot(miSlot.informacionSlot.id, miSlot.informacionSlot.id, this.transform, this.transform);
            }
        }
        else
        {
            inventario.IntercambiarSlot(miSlot.informacionSlot.id, miSlot.informacionSlot.id, this.transform, this.transform);
            inventario.RemoveItem(miSlot.informacionSlot.idItem, miSlot.informacionSlot);//Lo pone de nuevo en el slot del que proviene
        }

        imagen.raycastTarget = true;
        SlotDestino = null;
    }
}
