using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuConfiguracion : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    private const string MusicKey = "MusicVolume";
    private const string SFXKey = "SFXVolume";
    private const string LanguageKey = "language";
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] GameObject _menuConfigFirst;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
        float savedSFX = PlayerPrefs.GetFloat(SFXKey, 1f);

        volumeSlider.value = savedVolume;
        sfxSlider.value = savedSFX;

        SetVolume(savedVolume);
        SetSFX(savedSFX);

        volumeSlider.onValueChanged.AddListener(SetVolume);
        sfxSlider.onValueChanged.AddListener(SetSFX);

        EventSystem.current.SetSelectedGameObject(_menuConfigFirst);
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        Debug.Log("Current Refresh Rate: " + currentRefreshRate + "Hz");

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }

            if (filteredResolutions.Count == 0)
            {
                foreach (Resolution res in resolutions)
                {
                    bool exists = filteredResolutions.Exists(r => r.width == res.width && r.height == res.height);
                    if (!exists)
                    {
                        filteredResolutions.Add(res);
                    }
                }
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRateRatio.value.ToString("0.##") + "Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume (float musicVolume)
    {
        float volume = Mathf.Log10(Mathf.Max(musicVolume, 0.0001f)) * 20; // Evitar log(0)
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat(MusicKey, musicVolume);
        PlayerPrefs.Save();
    }

    public void SetSFX (float sfxVolume)
    {
        float volume = Mathf.Log10(Mathf.Max(sfxVolume, 0.0001f)) * 20; // Evitar log(0)
        audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat(SFXKey, sfxVolume);
        PlayerPrefs.Save();
    }
    
    public void Atras()
    {
        SceneManager.LoadScene("MenuInicio");
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void Salir()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
}
