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
        if(codigo[0] == 'P')
        {
            FindAnyObjectByType<SeguimientoMisionPrincipal>().AvanzarMision(codigo);
        }
        else
        {
            //Misiones secundaria
        }
    }
    public bool GetEstadoMision(string codigo)
    {
        if (codigo[0] == 'P')
        {
            return FindAnyObjectByType<SeguimientoMisionPrincipal>().GetEstadoMision(codigo);
        }
        else
        {
            //Misiones secundaria
            return false;
        }
    }
    public void ActualizarEstadoMision(string codigo)
    {
        if (codigo[0] == 'P')
        {
            FindAnyObjectByType<SeguimientoMisionPrincipal>().ActualizarMision(codigo);
        }
        else
        {
            //Misiones secundaria
        }
    }
    public bool RevisarRequisitos(string codigo)
    {
        if (codigo[0] == 'P')
        {
            return FindAnyObjectByType<SeguimientoMisionPrincipal>().EstanLosRequisitosCompletados(codigo);
        }
        else
        {
            //Misiones secundaria
            return false;
        }
    }
    public string GetMisionPrincipalActual()
    {
        return FindAnyObjectByType<SeguimientoMisionPrincipal>().GetMisionActual();
    }
    public bool EstaAceptadaLaMision(string codigo)
    {
        if(codigo[0] == 'P')
        {
            return FindAnyObjectByType<SeguimientoMisionPrincipal>().EstaMisionAceptada(codigo);
        }
        else
        {
            return false;
        }
    }
}
