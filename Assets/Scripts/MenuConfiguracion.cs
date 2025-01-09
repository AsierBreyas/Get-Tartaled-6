using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuConfiguracion : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume); 
    }
    public void Atras()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}
