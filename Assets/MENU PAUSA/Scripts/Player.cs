using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool MenuPausaActivo;
    public GameObject MenuPausa;

    public static Player llamar;

    public void Awake()
    {
        llamar = this;
    }
    void Start()
    {
        Sonido.llamar.PlayAmbientSound();//Cambiar cunado esten los onidos del juego
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuPausaActivo = !MenuPausaActivo;

            if (MenuPausaActivo)
            {
                Sonido.llamar.PlayMusic();
                MenuPausa.gameObject.SetActive(true);
            }
            else
            {
                MenuPausa.gameObject.SetActive(false);
                Sonido.llamar.StopMusic();
            }
        }
        
    }
}
