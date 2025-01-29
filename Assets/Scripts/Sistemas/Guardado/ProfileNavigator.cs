using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileNavigator : MonoBehaviour
{
    public void GoToNewGame()
    {
        SceneManager.LoadScene("MenuNuevoPerfil");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}