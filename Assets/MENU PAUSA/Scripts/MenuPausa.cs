using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para poder gestionar diferentes escenas
using UnityEngine.UI;
public class MenuPausa : MonoBehaviour
{
    public Slider volumen;
    public Slider efectos;
    public Slider brillo;
    public GameObject menu_Pausa;

    private void Start()
    {
        AudioManager.llamar.PlayMusic(AudioManager.llamar.musica);
    }

    private void Update()
    {
      
    }

    public void Continuar()
    {
        //AudioManager.llamar.StopMusic(AudioManager.llamar.musica);
        //menu_Pausa.SetActive(false);
    }
    public void Salir()
    {
        SceneManager.LoadScene("Escena menu principal"); //Me va a cargar la escena del juego
    }

    public void ResetVolumen()
    {
        volumen.value = 0.5f;
        efectos.value = 0.5f;
    }
    public void ResetBrillo()
    {
        brillo.value = 0.5f;
    }
}
