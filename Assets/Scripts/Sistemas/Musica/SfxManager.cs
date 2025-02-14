using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;

    [SerializeField] private AudioSource sfxSourcePrefab;
    private List<AudioSource> activeSources = new List<AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ReproducirSonido(AudioClip audioClip, float volume)
    {
        AudioSource availableSource = activeSources.Find(source => !source.isPlaying);

        if (availableSource == null)
        {
            availableSource = Instantiate(sfxSourcePrefab, transform);
            activeSources.Add(availableSource);
        }

        availableSource.clip = audioClip;
        availableSource.volume = volume;
        availableSource.PlayOneShot(audioClip);
    }
}
