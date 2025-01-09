using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuConfiguracion : MonoBehaviour
{
    public void SetVolume (float volume)
    {
        Debug.Log(volume);
    }

    public void Atras()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}
