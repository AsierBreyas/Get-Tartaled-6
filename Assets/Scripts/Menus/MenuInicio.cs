using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuInicio : MonoBehaviour
{
    [SerializeField] GameObject _menuFirst;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_menuFirst);
    }
    public void CargarJuego()
    {
        SceneManager.LoadScene("MenuPerfiles");
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
