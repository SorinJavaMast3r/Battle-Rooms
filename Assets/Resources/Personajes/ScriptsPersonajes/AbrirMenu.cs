using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirMenu : MonoBehaviour
{
    #region Prop menu pausa
    public bool MenuPausaActivo;
    public GameObject MenuPausa;
    public static AbrirMenu instanciar;
    #endregion

    public void Awake()
    {
        instanciar = this;
    }
    void Start()
    {
        Sonido.llamar.PlayAmbientSound();//Cambiar cuando esten los sonidos del juego
    }

    void Update()
    {
        AbrirMenuPausa();
    }

    public void AbrirMenuPausa()
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
