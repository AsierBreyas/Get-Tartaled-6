using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class NewGame : MonoBehaviour
{
    [SerializeField] TMP_InputField profileInput;
    [SerializeField] GameObject _menuFirst;
    
    // Objetos para pantalla de carga
    [SerializeField] Slider loadingBarFill;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject canvasMenuNuevoPerfil;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_menuFirst);
    }

    public void Generate()
    {
        string profileName = this.profileInput.text;
        ProfileStorage.CreateNewGame(profileName);

        // Mostrar pantalla de carga
        loadingScreen.SetActive(true);
        canvasMenuNuevoPerfil.SetActive(false);

        // Cargar la escena de la cinemática
        StartCoroutine(CargarJuegoAsync(3));
    }

    IEnumerator CargarJuegoAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false; // Evita que se active antes de tiempo

        while (operation.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.value = progressValue;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }

    public void Atras()
    {
        SceneManager.LoadScene("MenuPerfiles");
    }
}
