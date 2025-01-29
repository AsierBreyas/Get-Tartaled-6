using UnityEngine;

public class ColisionesTartalo : MonoBehaviour
{
    [SerializeField] float danioLobo = 1f;
    [SerializeField] float danioFuegoCerdo = 0.01f;
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("PUM quemao");
        FindFirstObjectByType<ControlesTartalo>().TakeDamage(danioFuegoCerdo);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Lobo")
        {
            FindFirstObjectByType<ControlesTartalo>().TakeDamage(danioLobo);
        }
    }
}
