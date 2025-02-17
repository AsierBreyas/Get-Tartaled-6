using UnityEngine;

public class Garrote : MonoBehaviour
{
    bool estoyEnMovimiento;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7 && other.tag != "Interactuable" && estoyEnMovimiento)
        {
            if(other.gameObject.GetComponent<Tartxalo>() != null)
            {
                FindFirstObjectByType<ControlesTartalo>().HeGolpeado(other.gameObject.GetComponent<Enemy>());
            }
            Debug.Log("Fallo mas que una escopeta de feria");
        }
    }
    public void EmpezarMovimiento()
    {
        estoyEnMovimiento = true;
    }
    public void TermineMovimiento()
    {
        estoyEnMovimiento = false;
    }
}
