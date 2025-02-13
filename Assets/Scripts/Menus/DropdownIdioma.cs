using UnityEngine;
using Assets.SimpleLocalization.Scripts;
using TMPro;

public class DropdownIdioma : MonoBehaviour
{
    [SerializeField] TMP_Dropdown languageDropdown;
    public void DropdownLanguage(int index)
    {
        switch (index)
        {
            case 0:
                LocalizationManager.Language = "Euskera";
                PlayerPrefs.SetString("SelectedLanguage", "Euskera");
                break;
            case 1:
                LocalizationManager.Language = "Spanish";
                PlayerPrefs.SetString("SelectedLanguage", "Spanish");
                break;
            default:
                LocalizationManager.Language = "Euskera";
                PlayerPrefs.SetString("SelectedLanguage", "Euskera");
                break;
        }

        PlayerPrefs.Save();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("SelectedLanguage"))
        {
            string savedLanguage = PlayerPrefs.GetString("SelectedLanguage");
            LocalizationManager.Language = savedLanguage;

            // Sincronizar dropdown con el idioma guardado
            int index = savedLanguage == "Euskera" ? 0 : 1;
            languageDropdown.value = index;
        }
    }
}
