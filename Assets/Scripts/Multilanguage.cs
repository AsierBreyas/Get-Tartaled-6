using UnityEngine;
using Assets.SimpleLocalization.Scripts;

public class Multilanguage : MonoBehaviour
{
    private void Awake()
    {
        LocalizationManager.Read();

        LocalizationManager.Language = "Euskera";
    }
}
