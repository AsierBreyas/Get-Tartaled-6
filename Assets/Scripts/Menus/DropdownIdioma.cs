using UnityEngine;
using Assets.SimpleLocalization.Scripts;

public class DropdownIdioma : MonoBehaviour
{
    public void DropdownLanguage(int index)
    {
        switch (index)
        {
            case 0:
                LocalizationManager.Language = "Euskera";
                break;
            case 1:
                LocalizationManager.Language = "Espaniol";
                break;
            default:
                LocalizationManager.Language = "Euskera";
                break;
        }
    }
}
