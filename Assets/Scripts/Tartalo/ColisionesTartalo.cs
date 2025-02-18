using UnityEngine;

public class ColisionesTartalo : MonoBehaviour
{
    [SerializeField] float danioLobo = 1f;
    [SerializeField] float danioFuegoCerdo = 0.03f;
    ControlesTartalo controles;

    private void Start()
    {
        controles = FindFirstObjectByType<ControlesTartalo>();
    }
    private void OnParticleCollision(GameObject other)
    {
       controles.TakeDamage(danioFuegoCerdo);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Lobo")
        {
            controles.TakeDamage(danioLobo);
        }
    }
    public void MeHanGolpeado(float daño)
    {
        controles.TakeDamage(daño);
    }
}
