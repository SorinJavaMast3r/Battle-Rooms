using UnityEngine;
using UnityEngine.UI;

public class Resolucion : MonoBehaviour
{
    #region Variables
    public Text Resolution_text;
    public Text Calidad_text;

    private int width;
    private int height;
    private bool PantallaCompl;
    private string calidad;

    private int contadorResolucion;
    private int contadorCalidad;
    #endregion

    #region Calidad
    public void SiguienteCalidad()
    {
        contadorCalidad++;
        Calidades();
    }
    public void AnteriorCalidad()
    {
        contadorCalidad--;
        Calidades();
    }
    public void Calidades()
    {
        contadorCalidad = Mathf.Clamp(contadorCalidad, 1, 5);

        switch (contadorCalidad)
        {
            case 1://Calidad más rápida
                calidad = "Muy baja";
                break;
            case 2://Calidad rápida
                calidad = "Baja";
                break;
            case 3:// Graficos simples
                calidad = "Media";
                break;
            case 4://Graficos buenos
                calidad = "Alta";
                break;
            case 5://Graficos ultra
                calidad = "Muy alta";
                break;
        }
        Calidad_text.text = calidad;
    }
    #endregion

    #region Resolucion
    public void SiguienteResolucion()
    {
        contadorResolucion++;
        Resoluciones();
    }
    public void AnteriorResolucion()
    {
        contadorResolucion--;
        Resoluciones();
    }
    public void PantallaCompleta()
    {
        PantallaCompl = !PantallaCompl;
    }
    public void Resoluciones()
    {
        contadorResolucion = Mathf.Clamp(contadorResolucion, 0, 3);

        switch (contadorResolucion)
        {
            case 0://mala
                width = 800;
                height = 600;
                break;
            case 1://720
                width = 1280;
                height = 720;
                break;
            case 2://1080p
                width = 1920;
                height = 1080;
                break;
            case 3://2k
                width = 2560;
                height = 1600;
                break;
        }
        Resolution_text.text = width.ToString() + " x " + height.ToString();
    }
    #endregion

    #region Aplicar
    public void Aplicar()
    {
        Screen.SetResolution(width, height, PantallaCompl);
        QualitySettings.SetQualityLevel(contadorCalidad, true);
        Screen.fullScreen = PantallaCompl;
        if (Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
            Screen.fullScreenMode = FullScreenMode.Windowed;
    }
    #endregion    
}
