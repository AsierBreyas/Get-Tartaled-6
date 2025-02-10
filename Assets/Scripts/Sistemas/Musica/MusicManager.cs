using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioMixer audioMixer;

    private const string VolumeKey = "MusicVolume"; // Guardar el volumen

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioMixer = GetComponent<AudioMixer>();
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
            if (audioSource.clip != gameMusic)
            {
                audioSource.clip = gameMusic;
                audioSource.Play();
            }
        }
        else if (scene.name == "MenuInicio")
        {
            if (audioSource.clip != menuMusic)
            {
                audioSource.clip = menuMusic;
                audioSource.Play();
            }
        }
    }

    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, -20f); // Valor por defecto -20
        audioMixer.SetFloat("volume", savedVolume);
    }
}
