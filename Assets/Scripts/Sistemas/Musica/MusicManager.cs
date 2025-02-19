using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource musicSource;

    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixerGroup musicGroup;

    private const string VolumeKey = "MusicVolume"; // Guardar el volumen

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al cambio de escena

            musicSource = GetComponent<AudioSource>();

            if (musicSource != null) 
            { 
                musicSource.outputAudioMixerGroup = musicGroup;
            }
            
            LoadVolume();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TartaloTerrain2" && musicSource.clip != gameMusic)
        {
            musicSource.clip = gameMusic;
            musicSource.Play();
        }
        else if (scene.name == "MenuInicio" && musicSource.clip != menuMusic)
        {
            musicSource.clip = menuMusic;
            musicSource.Play();
        }
        else if (scene.name == "Xabi") 
        { 
            musicSource.Stop();
        }
    }

    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, -20f); // Valor del volumen por defecto -20
        audioMixer.SetFloat("MusicVolume", savedVolume);
    }
}
