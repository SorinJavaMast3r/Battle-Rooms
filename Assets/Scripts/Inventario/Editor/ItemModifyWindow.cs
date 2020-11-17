using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;

public class ItemModifyWindow : EditorWindow
{
    private static Database database;
    private static EditorWindow ventana;
    private static Item databaseItem;
    private static Item nItem;

    private GUILayoutOption[] alinear = { GUILayout.MaxWidth(500), GUILayout.MinWidth(500) };
    public static void MostrarVentanaItem(Database db, Item item)
    {
        database = db;
        ventana = GetWindow<ItemModifyWindow>();//Obtenemos la ventana que acabamos de crear
        ventana.maxSize = new Vector2(300, 300);//Le doy unas medidas a la ventana
        ventana.minSize = new Vector2(300, 300);//Le doy unas medidas a la ventana
        databaseItem = item;
        nItem = new Item();
        nItem.id = item.id;
        nItem.nombre = item.nombre;
        nItem.descripcion = item.descripcion;
        nItem.estaqueable = item.estaqueable;
        nItem.imagenItem = item.imagenItem;
        nItem.tipo = item.tipo;
        nItem.estadisticas = item.estadisticas;
    }
    /*Se llama a OnGUI para renderizar y manejar eventos de GUI. OnGUI es la única función que puede implementar el sistema de 
     * GUI de "Modo Inmediato" (IMGUI) para representar y manejar eventos de GUI. Su implementación de OnGUI puede llamarse varias 
     * veces por cuadro (una llamada por evento). Para obtener más información sobre los eventos de la GUI, consulte la Referencia
     * de eventos . Si la propiedad habilitada de MonoBehaviour se establece en falso, no se llamará a OnGUI ().*/
    public void OnGUI()
    {
        MostrarItem(nItem);

        if (GUILayout.Button("Aceptar"))
            ModificarItem();
    }

    private bool deshabilitar;
    public void MostrarItem(Item item)
    {
        //Para que cuando pongamos texto salte a la siguiente linea y no continue todo en la misma
        GUIStyle estiloArea = new GUIStyle(GUI.skin.label);//Con "GUI.skin.label" obtengo el GUIStyle predeterminado de todas las etiquetas 
        estiloArea.wordWrap = true; // Se lo aplico a la etiqueta que quiera

        //Es igual que el anterio solo que he cambiado los layout y he puesto Field para poder poner valores a las propiedades
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Nombre: ");
        item.nombre = EditorGUILayout.TextField(item.nombre, alinear);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Imagen del item: ");
        item.imagenItem = (Sprite)EditorGUILayout.ObjectField(item.imagenItem, typeof(Sprite), false);//ObjectField es un campo donde asignamos un objeto. typeof(Sprite): Indicamos que el objeto que le pasamos es de tipo sprite y el false significa que no le pasasmos objetos de escena
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Tipo: ");
        item.tipo = (Item.TipoDeItem)EditorGUILayout.EnumPopup(item.tipo, alinear);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Estaqueable: ");
        item.estaqueable = EditorGUILayout.Toggle(item.estaqueable, alinear);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Descripcion: ");
        item.posicionScroll = EditorGUILayout.BeginScrollView(item.posicionScroll, GUILayout.MinHeight(3), GUILayout.MaxHeight(70));//PosicionScroll guarda la posicion en la que esta la barra de scroll
        item.descripcion = EditorGUILayout.TextArea(item.descripcion, estiloArea);
        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Daño: ");
        item.estadisticas.daño = EditorGUILayout.IntField(item.estadisticas.daño, alinear);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Defensa: ");
        item.estadisticas.defensa = EditorGUILayout.IntField(item.estadisticas.defensa, alinear);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Mana: ");
        item.estadisticas.mana = EditorGUILayout.IntField(item.estadisticas.mana, alinear);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    private void ModificarItem()
    {
        Undo.RecordObject(database, "Item modificado");//Estamos guardando el metodo undo para un cierto objeto
        databaseItem.nombre = nItem.nombre;
        databaseItem.descripcion = nItem.descripcion;
        databaseItem.estaqueable = nItem.estaqueable;
        databaseItem.tipo = nItem.tipo;
        databaseItem.imagenItem = nItem.imagenItem;
        databaseItem.estadisticas = nItem.estadisticas;
        EditorUtility.SetDirty(database);//sirva para decir al editor que el objeto que acabamos de modificar esta sucio y debe ser guardado cuando cerremos la escena
        ventana.Close();
    }
}
