using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    public void CargarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void CargarOpciones()
    {
        SceneManager.LoadScene("MenuInicioOpciones");
    }

    public void Salir()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
}
