using UnityEngine;

public class Garrote : MonoBehaviour
{
    bool estoyEnMovimiento;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && other.tag != "Interactuable" && estoyEnMovimiento)
        {
            Debug.Log("Pasta de dientes");
            FindFirstObjectByType<ControlesTartalo>().HeGolpeado(other.gameObject);
            Debug.Log(other.GetComponent<Tartxalo>());
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
