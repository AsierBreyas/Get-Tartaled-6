using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPausa : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject _pauseMenuFirst;

    public void MenuInicio()
    {
        SceneManager.LoadScene("MenuInicio");
    }

    void OnPausa(InputValue value)
    {
        Debug.Log("boton de pausa");
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(_pauseMenuFirst);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
