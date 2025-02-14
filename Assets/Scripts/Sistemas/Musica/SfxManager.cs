using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;

    [SerializeField] private AudioSource sfxSourcePrefab;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup sfxGroup;
    private List<AudioSource> activeSources = new List<AudioSource>();

    private const string SFXKey = "SFXVolume"; // Guardar el volumen de efectos de sonido

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSfx();
        }
    }

    private void LoadSfx()
    {
        float savedSFX = PlayerPrefs.GetFloat(SFXKey, -20f); // Valor del volumen por defecto -20
        audioMixer.SetFloat("SFXVolume", savedSFX);
    }

    public void ReproducirSonido(AudioClip audioClip, float volume)
    {
        AudioSource availableSource = activeSources.Find(source => !source.isPlaying);

        if (availableSource == null)
        {
            availableSource = Instantiate(sfxSourcePrefab, transform);
            if (sfxGroup != null) 
            { 
                availableSource.outputAudioMixerGroup = sfxGroup;
            }
            
            activeSources.Add(availableSource);
        }

        availableSource.clip = audioClip;
        availableSource.volume = volume;
        availableSource.PlayOneShot(audioClip);
    }
}
