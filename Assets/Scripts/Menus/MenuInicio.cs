using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class MenuInicio : MonoBehaviour
{
    [SerializeField] GameObject _menuFirst;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingBarFill;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_menuFirst);
    }
    public void CargarJuego()
    {
        //SceneManager.LoadScene("MenuPerfiles");
        StartCoroutine(CargarJuegoAsync("MenuPerfiles"));
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

    IEnumerator CargarJuegoAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBarFill.value = progressValue;

            yield return null;
        }
    }

}
