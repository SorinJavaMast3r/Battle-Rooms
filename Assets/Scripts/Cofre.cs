using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour
{
    #region Propiedades
    private Looteable loot;
    private bool cerrado = true;
    private bool areaCofre;

    private Animator anim;
    #endregion

    void Start()
    {
        loot = GetComponent<Looteable>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (areaCofre && Input.GetKeyDown(KeyCode.E))
        {
            if (cerrado)
            {
                anim.SetBool("abierto", true);
                loot.Lootear();
                cerrado = false;
            }
        }
        
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Player")
        {
            areaCofre = true;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        if (obj.tag == "Player")
        {
            areaCofre = false;
        }
    }
}
