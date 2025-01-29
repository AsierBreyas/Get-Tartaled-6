using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewGame : MonoBehaviour
{
    [SerializeField] TMP_InputField profileInput;

    public void Generate()
    {
        string profileName = this.profileInput.text;
        ProfileStorage.CreateNewGame(profileName);

        SceneManager.LoadScene("TartaloTerrain2");
    }
}
