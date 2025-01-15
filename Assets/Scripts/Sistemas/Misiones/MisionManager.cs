using UnityEngine;

public class MisionManager : MonoBehaviour
{
    public static MisionManager instancia { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this);
        }
        else
        {
            instancia = this;
        }
    }
    public void AvanzarMision(string codigo)
    {
        if(codigo == "P-000")
        {
            FindAnyObjectByType<SeguimientoMisionPrincipal>().AvanzarMision();
        }
        else
        {
            //Misiones secundaria
        }
    }
}
