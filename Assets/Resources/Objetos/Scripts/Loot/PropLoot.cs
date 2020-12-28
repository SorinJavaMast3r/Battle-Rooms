using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "BD Loot")]//Genera una base de datos
public class PropLoot : ScriptableObject
{
    [System.Serializable]//Para poder verla en el inspector
    public struct Probabilidades
    {
        public int rareza; //Cuanto mas comun sea este numero mas facil es esque te salga
        public GameObject item;//Prefab del objeto
    }

    public Probabilidades[] prob;
}
