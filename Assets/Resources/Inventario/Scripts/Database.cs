using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Systema de Inventario/Database")]//Instancia 
public class Database : ScriptableObject
{
    public List<Item> listaItems = new List<Item>();

    public Item FindItemInDatabase(int id)
    {
        foreach (Item item in listaItems)
        {
            if (item.id == id)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Item
{
    public int id;
    public string nombre;
    [TextArea(3, 3)]
    public string descripcion;
    public bool estaqueable;
    public TipoDeItem tipo;
    public Estadisticas estadisticas;
    public Vector2 posicionScroll;
    public Sprite imagenItem;

    [System.Serializable]
    public struct Estadisticas
    {
        public int daño;
        public int mana;
        public int defensa;
    }

    public enum TipoDeItem { consumible, armadura, arma, otro }
}