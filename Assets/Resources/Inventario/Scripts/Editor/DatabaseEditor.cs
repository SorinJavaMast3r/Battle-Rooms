using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Database))]
public class DatabaseEditor : Editor
{
    private Database database;//Referencia a la base de datos

    private string buscarString = "";//Se pone vacio porq puede dar error que al añadir un item no se aplique lo de busqueda porq lo igualaria al valor anterior y seria null
    private bool buscar;
    private void OnEnable()//A esta funcion se la llama cuando hacemos click en un elementos de tipo database
    {
        database = (Database)target;//la base de  datos es igual al elemento en el cual se hizo click tranformado en database. target es un elemto de la clase Editor y es igual al elemento al que le hice click 
    }

    //Metodo para organizar la vista de la Bd en el inspector
    public override void OnInspectorGUI()
    {
        
        if (database)
        {
            //Creamos un aetiqueta que nos permite ver la cantidad de elementos que tenemos.
            //Todo lo que este entre estas dos instrucciones se va a renderizar en el inspector una al lado de la otra(Cualquier control se  va renderizar uno al lado del otro)
            EditorGUILayout.BeginHorizontal("Box");//Box es un estilo predeterminado que dibuja un recuadro
            GUILayout.Label("Cantidad de items en la BD: " + database.listaItems.Count);//Nos devuelve cuantos items tenemos
            EditorGUILayout.EndHorizontal();

            //Etiqueta que nos va a permitir buscar un item
            if (database.listaItems.Count > 0)
            {
                EditorGUILayout.BeginHorizontal("Box");
                GUILayout.Label("Buscar: ");
                buscarString = GUILayout.TextField(buscarString);//Me almacen lo que busque en la variable y para la siguiente vez que busque va a tomar el valor que tenia antes
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Añadir item"))
            {
                Debug.Log("Abrir ventana");
                ItemWindow.MostrarVentanaVacia(database);
            }

            if (System.String.IsNullOrEmpty(buscarString))
            {
                buscar = false;
            }
            else
            {
                buscar = true;
            }



            //Recorre todos los items de la BD y los dibuja en el inspector con el diseño creado
            foreach (Item item in database.listaItems)
            {
                if (buscar)
                {
                    if (item.nombre == buscarString || item.nombre.Contains(buscarString) || item.id.ToString() == buscarString)                    
                        MostrarItem(item);                    
                }
                else                
                    MostrarItem(item);                
            }

            if (itemBorrado != null)//Hacemos esto aqui para borrar el item fuera del foreach y no de problemas
                database.listaItems.Remove(itemBorrado);            
        }
    }

    private Item itemBorrado;

    public void MostrarItem(Item item)
    {
        //Para que cuando pongamos texto salte a la siguiente linea y no continue todo en la misma
        GUIStyle estiloEtiqueta = new GUIStyle(GUI.skin.label);//Con "GUI.skin.label" obtengo el GUIStyle predeterminado de todas las etiquetas 
        estiloEtiqueta.wordWrap = true; // Se lo aplico a la etiqueta que quiera

        GUIStyle estiloNumero = new GUIStyle(GUI.skin.label);//Con "GUI.skin.label" obtengo el GUIStyle predeterminado de todas las etiquetas 
        estiloNumero.wordWrap = true;
        estiloNumero.alignment = TextAnchor.MiddleLeft;
        estiloNumero.fixedWidth = 70;
        estiloNumero.margin = new RectOffset(0,50,0,0);

        //Cada propiedad que tenga nuestro item estara dibujada en un recuadro pero que cada propiedad que este en ese recuadro este una encima de la otra
        //Pongo los horizontal para que cada uno este al lado del otro
        EditorGUILayout.BeginVertical("Box");

        Sprite spriteItem = item.imagenItem;//Obtengo una copia de la imagen del item en itemSprite y veo si tiene asignada una imagen. En caso de que si muestro el nombre de la imagen
        if (spriteItem != null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Imagen:" + item.imagenItem.ToString());
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("ID: ");
        GUILayout.Label(item.id.ToString(), estiloNumero);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Nombre: ");
        GUILayout.Label(item.nombre, estiloEtiqueta);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Tipo: ");
        GUILayout.Label(item.tipo.ToString(), estiloEtiqueta);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Estaqueable: ");
        GUILayout.Label(item.estaqueable.ToString(), estiloEtiqueta);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Descripcion: ");
        item.posicionScroll = EditorGUILayout.BeginScrollView(item.posicionScroll, GUILayout.MinHeight(3), GUILayout.MaxHeight(70));//PosicionScroll guarda la posicion en la que esta la barra de scroll
        GUILayout.Label(item.descripcion, estiloEtiqueta);
        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Daño: ");
        GUILayout.Label(item.estadisticas.daño.ToString(), estiloNumero);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Defensa: ");
        GUILayout.Label(item.estadisticas.defensa.ToString(), estiloNumero);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Mana: ");
        GUILayout.Label(item.estadisticas.mana.ToString(), estiloNumero);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Modificar"))
            ItemModifyWindow.MostrarVentanaItem(database, item);

        if (GUILayout.Button("Borrar"))
            itemBorrado = item;
        else
            itemBorrado = null;

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }
}
