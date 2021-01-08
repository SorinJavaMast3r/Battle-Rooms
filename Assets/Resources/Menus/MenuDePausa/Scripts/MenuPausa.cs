using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para poder gestionar diferentes escenas
using UnityEngine.UI;
public class MenuPausa : MonoBehaviour
{
    public Slider musica;
    public Slider efectos;
    public Slider sonidoAmbiente;

    public void Continuar()
    {
        this.gameObject.SetActive(false);
        Sonido.llamar.StopMusic();
        arissaController.instanciar.MenuPausaActivo = false;
    }
    public void Salir()
    {
        SceneManager.LoadScene("MainMenu"); //Me va a cargar la escena del juego
    }

    public void ResetVolumen()
    {
        musica.value = 0.5f;
        efectos.value = 0.5f;
        sonidoAmbiente.value = 0.5f;
    }
}
