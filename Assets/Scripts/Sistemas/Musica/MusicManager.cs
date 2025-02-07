using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    public AudioClip gameMusic;
    public AudioMixerGroup audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioMixer = GetComponent<AudioMixerGroup>();
            SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al cambio de escena
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
    }
}
