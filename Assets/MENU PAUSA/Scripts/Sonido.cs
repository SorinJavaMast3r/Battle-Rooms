using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sonido : MonoBehaviour
{
    #region Variables
    public AudioSource musicSource, effectSource, AmbientSource;

    public Text musica_texto, efecto_texto, SonidoAmbiente_texto;

    public AudioMixer controlAudio;    

    public Slider SliderMusica, SliderEfectos, SliderAmbiente;

    public static Sonido llamar;
    #endregion

    #region Iniciar
    /*El awake se ejecuta primero que el start y sirve para obtener las referencias a otros objetos, variables, script 
    y de clases que vamos a utilizar en este script. Las referencias las ponemos en este metodo porque necesitamos que 
    carguen primero. En el start podemos inicializarlas y cambiarlas el valor*/
    public void Awake()//Con este metodo permitimos acceder a este audioManager desde cualquier script
    {
        llamar = this;        
    }

    private void Start()
    {        
    }
    #endregion

    #region Sliders
    public void Volumen(float valor)
    {
        controlAudio.SetFloat("VolumenAmbiente", Mathf.Log10(valor) * 20);  //Se pone logaritmop porque no es exponencial y hay que convertirlo en exponencial. Ponemos *20 porque los decibelios que dan son 4 5 en vez de 40 o 50 por eso los multiplico por 20 (0.0001 -> -40)
    }
    
    public void Musica(float valor)
    {
        controlAudio.SetFloat("VolumenMusica", Mathf.Log10(valor) * 20);
    }

    public void Efectos(float valor)
    {
        controlAudio.SetFloat("VolumenEfectos", Mathf.Log10(valor) * 20);
    }
    #endregion

    #region Iniciar sonidos
    public void PlayMusic()
    {        
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Pause();
    }

    public void PlayEffect(AudioClip efecto)
    {
        effectSource.PlayOneShot(efecto);
    }  

    public void PlayAmbientSound()
    {
        AmbientSource.Play();
    } //Modificar
    #endregion

    #region Guardar y aztualizar
    public void MusicUpdate()
    {
        musicSource.volume = SliderMusica.value;
        musica_texto.text = Mathf.RoundToInt(SliderMusica.value * 100).ToString();

        
        PlayerPrefs.SetFloat("Volumen_musica", musicSource.volume); //Es un sitio donde podemos guardar valores
        PlayerPrefs.SetString("Texto_musica", musica_texto.text);

        PlayerPrefs.Save();//Para que simpre que modifiquemos estos valores, los guarde
    }

    public void EffectUpdate()
    {
        effectSource.volume = SliderEfectos.value;
        efecto_texto.text = Mathf.RoundToInt(SliderEfectos.value * 100).ToString();

        PlayerPrefs.SetFloat("Volumen_efectos", effectSource.volume);
        PlayerPrefs.SetString("Texto_efectos", efecto_texto.text);

        PlayerPrefs.Save();//Para que simpre que modifiquemos estos valores, los guarde
    }

    public void AmbientSoundUpdate()
    {
        AmbientSource.volume = SliderAmbiente.value;
        SonidoAmbiente_texto.text = Mathf.RoundToInt(SliderAmbiente.value * 100).ToString();

        PlayerPrefs.SetFloat("Volumen_ambiente", AmbientSource.volume);
        PlayerPrefs.SetString("Texto_ambiente", SonidoAmbiente_texto.text);

        PlayerPrefs.Save();//Para que simpre que modifiquemos estos valores, los guarde
    }

    public void InicializarVolumen()
    {
        musicSource.volume = PlayerPrefs.GetFloat("Volumen_musica", 0.5f); //Ponemos 0.5f para que en el caso de que no encuentr nada no de error si no que ponga eso
        effectSource.volume = PlayerPrefs.GetFloat("Volumen_efectos", 0.5f);
        AmbientSource.volume = PlayerPrefs.GetFloat("Volumen_ambiente", 0.5f);

        musica_texto.text = PlayerPrefs.GetString("Texto_musica", "50");
        efecto_texto.text = PlayerPrefs.GetString("Texto_efectos", "50");
        SonidoAmbiente_texto.text = PlayerPrefs.GetString("Texto_ambiente", "50");

        SliderMusica.value = musicSource.volume;
        SliderEfectos.value = effectSource.volume;
        SliderAmbiente.value = AmbientSource.volume;
    }
    #endregion
}
