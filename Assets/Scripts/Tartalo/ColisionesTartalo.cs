using UnityEngine;

public class ColisionesTartalo : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("PUM quemao");
        FindFirstObjectByType<ControlesTartalo>().TakeDamage(0.33f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wolve")
        {
            FindFirstObjectByType<ControlesTartalo>().TakeDamage(20f);
        }
    }
}
