using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject generalCanvas;
    public void ItsGameOver()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        generalCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}
