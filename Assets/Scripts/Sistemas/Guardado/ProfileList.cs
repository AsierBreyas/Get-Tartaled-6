using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ProfileList : MonoBehaviour
{
    public Transform profilesHolder;

    public GameObject profileUIBoxPrefab;

    // Objetos para pantalla de carga
    [SerializeField] GameObject otherCanvas;
    [SerializeField] Slider loadingBarFill;
    [SerializeField] GameObject loadingScreen;

    private void Start()
    {
        var index = ProfileStorage.GetProfileIndex();

        foreach (var profileName in index.profileFileNames)
        {
            var go = Instantiate(this.profileUIBoxPrefab);
            var uibox = go.GetComponent<ProfileBoxUI>();

            uibox.nameLabel.text = profileName;

            // Click boton de cargar
            uibox.loadBtn.onClick.AddListener(() =>
            {
                ProfileStorage.LoadProfile(profileName);
                StartCoroutine(CargarJuegoAsync(2));
            });

            // Click boton de borrar
            uibox.deleteBtn.onClick.AddListener(() =>
            {
                ProfileStorage.DeleteProfile(profileName);
                Destroy(go);
            });

            go.transform.SetParent(this.profilesHolder, false);
        }
    }

    IEnumerator CargarJuegoAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreen.SetActive(true);
        otherCanvas.SetActive(false);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBarFill.value = progressValue;

            yield return null;
        }
    }
}
