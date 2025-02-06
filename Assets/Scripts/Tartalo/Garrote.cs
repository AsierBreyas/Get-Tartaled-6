using UnityEngine;

public class Garrote : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7 && other.tag != "Interactuable")
        {
            FindFirstObjectByType<ControlesTartalo>().HeGolpeado(other.gameObject.GetComponent<Enemy>());
        }
    }
}
