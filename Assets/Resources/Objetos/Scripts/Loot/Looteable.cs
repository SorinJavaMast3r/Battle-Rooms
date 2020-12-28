using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looteable : MonoBehaviour
{
    [SerializeField]
    private Transform localizacion;

    [SerializeField]
    [Range(0, 100)]
    private int probSoltar;

    [SerializeField]
    private int cantidadMaxima = 2;

    [SerializeField]
    private int bonus = 1;

    void Start()
    {
        if (localizacion == null)
        {
            localizacion = transform;
        }
    }
    
    public void Lootear()
    {
        Loot.instanciar.Spawnear(localizacion, bonus, cantidadMaxima, probSoltar);
    }
}
