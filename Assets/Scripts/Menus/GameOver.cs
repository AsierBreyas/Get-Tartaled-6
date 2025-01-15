using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject healthbarCanvas;
    public void RecargarJuego()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Josu");
        gameOverCanvas.SetActive(false);
        healthbarCanvas.SetActive(true);

    }
}
