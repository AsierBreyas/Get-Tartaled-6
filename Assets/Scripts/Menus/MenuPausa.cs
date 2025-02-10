using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuPausa : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject configMenuUI;
    [SerializeField] GameObject _pauseMenuFirst;
    [SerializeField] GameObject _configMenuFirst;
    [SerializeField] GameObject gameOverMenu;

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
        else if (!GameIsPaused && !gameOverMenu.activeSelf)
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        configMenuUI.SetActive(false);
        Cursor.visible = false;
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

    public void Config()
    {
        pauseMenuUI.SetActive(false);
        configMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_configMenuFirst);
    }

    public void AtrasConfig()
    {
        pauseMenuUI.SetActive(true);
        configMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_pauseMenuFirst);
    }
}
