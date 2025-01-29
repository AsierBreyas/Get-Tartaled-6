using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileList : MonoBehaviour
{
    public Transform profilesHolder;

    public GameObject profileUIBoxPrefab;

    private void Start()
    {
        var index = ProfileStorage.GetProfileIndex();

        foreach (var profileName in index.profileFileNames)
        {
            var go = Instantiate(this.profileUIBoxPrefab);
            var uibox = go.GetComponent<ProfileBoxUI>();

            uibox.nameLabel.text = profileName;

            // Click load button
            uibox.loadBtn.onClick.AddListener(() =>
            {
                ProfileStorage.LoadProfile(profileName);
                SceneManager.LoadScene("TartaloTerrain2");
            });

            // Click delete button
            uibox.deleteBtn.onClick.AddListener(() =>
            {
                ProfileStorage.DeleteProfile(profileName);
                Destroy(go);
            });

            go.transform.SetParent(this.profilesHolder, false);
        }
    }
}
