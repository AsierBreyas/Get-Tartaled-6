using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProfileList : MonoBehaviour
{
    public Transform profilesHolder;

    public GameObject profileUIBoxPrefab;

    // Objetos para pantalla de carga
    [SerializeField] Slider loadingBarFill;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject btn_NuevaPartida;
    [SerializeField] GameObject canvasPerfiles;

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

                if (ProfileStorage.s_currentProfile == null)
                {
                    Debug.LogError("ERROR: El perfil no se cargó correctamente. No se iniciará la carga de la escena.");
                    return;
                }
                Debug.Log($"Perfil cargado: {ProfileStorage.s_currentProfile.fileName}, Posición ({ProfileStorage.s_currentProfile.x}, {ProfileStorage.s_currentProfile.y}, {ProfileStorage.s_currentProfile.z})");

                StartCoroutine(CargarJuegoAsync(2));
            });

            // Click boton de borrar
            uibox.deleteBtn.onClick.AddListener(() =>
            {
                ProfileStorage.DeleteProfile(profileName);
                Destroy(go);
                EventSystem.current.SetSelectedGameObject(btn_NuevaPartida);
            });

            go.transform.SetParent(this.profilesHolder, false);
        }
    }

    IEnumerator CargarJuegoAsync(int sceneId)
    {
        loadingScreen.SetActive(true);
        canvasPerfiles.SetActive(false);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        //operation.allowSceneActivation = false; // Evita que la escena se active inmediatamente

        float timer = 0f;

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.value = progressValue;

            timer += Time.deltaTime;
            yield return null;
        }
        operation.allowSceneActivation = true; // Activa la escena después de 5 segundos
    }

}
