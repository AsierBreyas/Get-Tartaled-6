using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject healthbarCanvas;
    [SerializeField] GameObject _gameoverMenuFirst;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_gameoverMenuFirst);
    }
    public void RecargarJuego()
    {
        Time.timeScale = 1;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        gameOverCanvas.SetActive(false);
        healthbarCanvas.SetActive(true);

    }
}
