using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForCinematicToEnd());
    }

    private IEnumerator WaitForCinematicToEnd()
    {
        yield return new WaitForSeconds(60); // Espera los 10 segundos de la cinemática
        SceneManager.LoadScene(2); // Carga la escena del juego
    }
}
