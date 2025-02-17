using UnityEngine;

public class ArmaEnemigo : MonoBehaviour
{
    [SerializeField]
    float daño;
    bool estoyAtacando;
    bool heHechoDaño;
    ControlesTartalo controles;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && estoyAtacando && !heHechoDaño)
        {
            if (controles == null)
                controles = FindAnyObjectByType<ControlesTartalo>();
            heHechoDaño = true;
            controles.TakeDamage(daño);
        }
    }
    public void EmpeceElAtaque()
    {
        estoyAtacando = true;
        heHechoDaño = false;
    }
    public void TermineElAtaque()
    {
        estoyAtacando = false;
    }
}
