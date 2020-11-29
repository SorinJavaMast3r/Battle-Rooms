using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource effectSource;
    public AudioSource musicSource;

    public AudioClip musica;
    public AudioClip efecto;

    public Slider musicaSlider;
    public Slider effectSlider;

    public Text musica_text;
    public Text efectos_Text;

    public static AudioManager llamar;

    /*El awake se ejecuta primero que el start y sirve para obtener las referencias a otros objetos, variables, script 
    y de clases que vamos a utilizar en este script. Las referencias las ponemos en este metodo porque necesitamos que 
    carguen primero. En el start podemos inicializarlas y cambiarlas el valor*/
    public void Awake()//Con este metodo permitimos acceder a este audioManager desde cualquier script
    {
        llamar = this;
        InicializarVolumen();
    }
    public void PlayEffect()
    {
        effectSource.PlayOneShot(efecto);
    }

    public void PlayMusic(AudioClip musica)
    {
        musicSource.clip = musica;
        musicSource.Play();
    }
    public void StopMusic(AudioClip musica)
    {
        musicSource.clip = musica;
        musicSource.Stop();
    }

    public void MusicUpdate()
    {
        musicSource.volume = musicaSlider.value;
        musica_text.text = Mathf.RoundToInt(musicaSlider.value * 100).ToString();

        PlayerPrefs.SetFloat("Volumen_musica", musicSource.volume); //Es un sitio donde podemos guardar valores
        PlayerPrefs.SetString("Texto_musica", musica_text.text);

        PlayerPrefs.Save();//Para que simpre que modifiquemos estos valores, los guarde
    }

    public void EffectUpdate()
    {
        effectSource.volume = effectSlider.value;
        efectos_Text.text = Mathf.RoundToInt(effectSlider.value * 100).ToString();

        PlayerPrefs.SetFloat("Volumen_efectos", effectSource.volume);
        PlayerPrefs.SetString("Texto_efectos", efectos_Text.text);

        PlayerPrefs.Save();//Para que simpre que modifiquemos estos valores, los guarde
    }

    private void InicializarVolumen()
    {
        musicSource.volume = PlayerPrefs.GetFloat("Volumen_musica", 0.5f); //Ponemos 0.5f para que en el caso de que no encuentr nada no de error si no que ponga eso
        effectSource.volume = PlayerPrefs.GetFloat("Volumen_efectos", 0.5f);

        musica_text.text = PlayerPrefs.GetString("Texto_musica");
        efectos_Text.text = PlayerPrefs.GetString("Texto_efectos");

        musicaSlider.value = musicSource.volume;
        effectSlider.value = effectSource.volume;
    }
}
