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
        StartCoroutine(CargarJuegoAsync(3));
        Invoke("PasarCinematica", 10);
    }

    IEnumerator CargarJuegoAsync(int sceneId)
    {
        loadingScreen.SetActive(true); // Activa la pantalla de carga
        canvasMenuNuevoPerfil.SetActive(false);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false; // Evita que la escena se active inmediatamente

        float timer = 0f;

        while (timer < 5f || operation.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.value = progressValue;

            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true; // Activa la escena después de 5 segundos
    }

    void PasarCinematica()
    {
        SceneManager.LoadScene(2);
    }

    public void Atras()
    {
        SceneManager.LoadScene("MenuPerfiles");
    }
}
