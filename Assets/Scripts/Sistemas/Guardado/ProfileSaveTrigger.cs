using UnityEngine;

public class ProfileSaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ProfileStorage.StorePlayerProfile(other.gameObject);
            Debug.Log("Guardado");
        }
    }
}