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
        yield return new WaitForSeconds(61);
        SceneManager.LoadScene(2);
    }
}
