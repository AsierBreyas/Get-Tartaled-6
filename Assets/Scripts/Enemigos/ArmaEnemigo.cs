using UnityEngine;

public class ArmaEnemigo : MonoBehaviour
{
    [SerializeField]
    float daño;
    bool estoyAtacando;
    ControlesTartalo controles;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && estoyAtacando)
        {
            if (controles == null)
                controles = FindAnyObjectByType<ControlesTartalo>();
            controles.TakeDamage(daño);
        }
    }
    public void EmpeceElAtaque()
    {
        estoyAtacando = true;
    }
    public void TermineElAtaque()
    {
        estoyAtacando = false;
    }
}
