using UnityEngine;

public class Garrote : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindFirstObjectByType<ControlesTartalo>().HeGolpeado();
    }
}
