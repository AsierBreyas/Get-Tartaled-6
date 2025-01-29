using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject generalCanvas;

    // Datos guardado
    [SerializeField] GameObject player;

    private void Start()
    {
        if (!ProfileStorage.s_currentProfile.newGame)
        {
            // Cargar partida
            float x = ProfileStorage.s_currentProfile.x;
            float y = ProfileStorage.s_currentProfile.y;
            float z = ProfileStorage.s_currentProfile.z;
            Vector3 pos = new Vector3(x, y, z);
            player.transform.Translate(pos);
        }
    }
    public void ItsGameOver()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        generalCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
}
