using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ProfileNavigator : MonoBehaviour
{
    [SerializeField] GameObject _menuFirst;
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_menuFirst);
    }
    public void GoToNewGame()
    {
        SceneManager.LoadScene("MenuNuevoPerfil");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}