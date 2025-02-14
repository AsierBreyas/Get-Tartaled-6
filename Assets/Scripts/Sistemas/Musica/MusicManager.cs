using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource musicSource;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioMixer audioMixer;

    private const string VolumeKey = "MusicVolume"; // Guardar el volumen

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al cambio de escena

            LoadVolume();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TartaloTerrain2")
        {
            if (musicSource.clip != gameMusic)
            {
                musicSource.clip = gameMusic;
                musicSource.Play();
            }
        }
        else if (scene.name == "MenuInicio")
        {
            if (musicSource.clip != menuMusic)
            {
                musicSource.clip = menuMusic;
                musicSource.Play();
            }
        }
    }

    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, -20f); // Valor del volumen por defecto -20
        audioMixer.SetFloat("volume", savedVolume);
    }
}
