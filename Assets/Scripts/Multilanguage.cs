using UnityEngine;
using Assets.SimpleLocalization.Scripts;

public class Multilanguage : MonoBehaviour
{
    private void Awake()
    {
        LocalizationManager.Read();

        // Cargar el idioma guardado o establecer "Euskera" si no hay ninguno
        if (PlayerPrefs.HasKey("SelectedLanguage"))
        {
            LocalizationManager.Language = PlayerPrefs.GetString("SelectedLanguage");
        }
        else
        {
            LocalizationManager.Language = "Euskera";
            PlayerPrefs.SetString("SelectedLanguage", "Euskera");
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("SelectedLanguage"))
        {
            string savedLanguage = PlayerPrefs.GetString("SelectedLanguage");
            LocalizationManager.Language = savedLanguage;
        }
    }
}
