using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField]
    private PropLoot propLoot;

    [SerializeField]
    private int probabilidad;

    [SerializeField]
    private int sumaRarezas;

    private PropLoot.Probabilidades[] arrayOrdenado;

    public static Loot instanciar { get; private set; }

    private void Awake()
    {
        instanciar = this;
    }

    void Start()
    {
        foreach (var i in propLoot.prob)
        {
            sumaRarezas += i.rareza;//Me suma todas las rarezas de todos los items
        }

        arrayOrdenado = propLoot.prob; //Igualamos el array que hemos creado nuevo al de la BD de propLoot.
        System.Array.Sort(arrayOrdenado, new Comparador()); //Ordena la estructura de Probabilidades segun la rareza por eso creamos un comparador para que me compare las Probabilidades segun la rareza y no por estructuras. 
        System.Array.Reverse(arrayOrdenado);//El sort ordena de menos a mayor y con el reverse le damos la vuelta para que este de mayor a menor.       
    }

    public void Spawnear(Transform localizacion, int bonus, int cantidadLoot, int probSoltar, bool? repeated = null)
    {
        probabilidad = Random.Range(0, (sumaRarezas + 1));//Cogemos una probabilidad random que sea desde el 0 al valor de la sumaRarezas
        int miProbabilidad = probabilidad * bonus; //Mi probabilidad va a ser la que hemos calculado mas el bonus. (El bonus sera de uno menos en el boss final que sera de mas y aumenta las posibilidades de que me toquen mas cosas)

        if (repeated != null)
        {
            if (repeated == true)
            {
                for (int i = 0; i < arrayOrdenado.Length; i++)
                {
                    if (miProbabilidad <= arrayOrdenado[i].rareza)
                    {
                        GameObject item = Instantiate(arrayOrdenado[i].item, localizacion.position, Quaternion.identity);

                        if (cantidadLoot == 1)
                        {
                            return;
                        }
                        cantidadLoot--;
                    }
                    else
                    {
                        miProbabilidad -= arrayOrdenado[i].rareza;
                    }
                }
            }
        }

        int calcularSoltar = Random.Range(0, 101);// Elegimos un porcentaje random de las posibilidades que teneos que nos droppe un item
        if (calcularSoltar >= probSoltar)// Si el porcentaje es mayor que el que le hemos pasado no va a droppear nada
        {
            print("No hay loot");
            return;//ya no sigue el script
        }

        if (miProbabilidad >= sumaRarezas)//Para que si le metemos un factor adicional y se pasa de la suma de rarezas que la iguale a la misma
        {
            miProbabilidad = sumaRarezas;
        }

        for (int i = 0; i < arrayOrdenado.Length; i++)
        {
            if (miProbabilidad <= arrayOrdenado[i].rareza)
            {
                GameObject item = Instantiate(arrayOrdenado[i].item, localizacion.position, Quaternion.identity);

                if (cantidadLoot == 1)
                {
                    return;
                }
                cantidadLoot--;
            }
            else
            {
                miProbabilidad -= arrayOrdenado[i].rareza;
            }                        
        }

        if (cantidadLoot >= 1)
        {
            Spawnear(localizacion, bonus, cantidadLoot, probSoltar, true);
        }

    }   
}

public class Comparador : IComparer
{
    public int Compare(object x, object y)
    {
        int rareza1 = ((PropLoot.Probabilidades)x).rareza;
        int rareza2 = ((PropLoot.Probabilidades)y).rareza;

        if (rareza1 == rareza2)
        {
            return 0;
        }
        else if (rareza1 > rareza2)
        {
            return 1;            
        }
        else
        {
            return -1;
        }
    }
}
