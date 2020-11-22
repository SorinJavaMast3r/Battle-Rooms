using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Para poder gestionar diferentes escenas
public class MenuPausa : MonoBehaviour
{
    public void Jugar()
    {
        

    }

    public void Salir()
    {
        SceneManager.LoadScene("Escena menu principal"); //Me va a cargar la escena del juego
    }
}
