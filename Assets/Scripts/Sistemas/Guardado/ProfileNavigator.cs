using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileNavigator : MonoBehaviour
{
    public void GoToNewGame()
    {
        SceneManager.LoadScene("NuevoPerfil");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}