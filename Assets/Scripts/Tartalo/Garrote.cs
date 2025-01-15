using UnityEngine;

public class Garrote : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Lobo")
        {
            FindFirstObjectByType<ControlesTartalo>().HeGolpeado();
        }
    }
}
