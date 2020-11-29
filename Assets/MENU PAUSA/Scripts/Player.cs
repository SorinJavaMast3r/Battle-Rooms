using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool MenuPausaActivo;
    public GameObject MenuPausa;
    void Start()
    {
        MenuPausaActivo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPausaActivo = !MenuPausaActivo;
        }
        if (MenuPausaActivo)
        {
            MenuPausa.gameObject.SetActive(true);            
        }
        else
        {
            MenuPausa.gameObject.SetActive(false);
        }
    }
}
