using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NewGame : MonoBehaviour
{
    [SerializeField] TMP_InputField profileInput;
    
    // Objetos para pantalla de carga
    [SerializeField] Slider loadingBarFill;
    [SerializeField] GameObject loadingScreen;

    public void Generate()
    {
        string profileName = this.profileInput.text;
        ProfileStorage.CreateNewGame(profileName);
        StartCoroutine(CargarJuegoAsync(2));
    }

    IEnumerator CargarJuegoAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBarFill.value = progressValue;

            yield return null;
        }
    }
}
