using System.Collections.Generic;
using UnityEngine;

public class SeguimientoMisionPrincipal : MonoBehaviour
{
    public static SeguimientoMisionPrincipal instancia { get; private set; }
    List<Mision> seguimiento = new List<Mision>();
    int misionActual;
    void Start()
    {
        if(instancia != null && instancia != this)
        {
            Destroy(this);
        }
        else
        {
            instancia = this;
        }
        seguimiento.Add(new Mision("Hablar con Izaro", "Ve al pueblo a contarle lo que has soñado a Izaro", "P-000"));
        seguimiento.Add(new Mision("Ve a la huerta", "Ve a la huerta de amoña a recoger las lechugas y los huevos, seguramente Izaro este alli", "P-001"));
        seguimiento.Add(new Mision("Mata a los jabalís", "Mata a los jabalís que se estan comiendo las cosechas", "P-002"));
        seguimiento[2].SetEnemigosAMatar(3, 0);
        seguimiento.Add(new Mision("Llevale las sobras a amoña y preguntarle por la amiga", "Entregale la lechuga y el huevo a amoña", "P-003"));
        seguimiento.Add(new Mision("Busca a amiga de Izaro", "Explora el pueblo en busca de Zaloa", "P-004"));
        seguimiento.Add(new Mision("¡Protege la aldea!", "Acaba con los cerdos", "P-005"));
        seguimiento[5].SetEnemigosAMatar(12, 0);
        seguimiento.Add(new Mision("Obten información sobre el ataque", "Los cerdos es raro que ataquen la aldea, alguien tiene que estar detrás de esto", "P-006"));
        seguimiento.Add(new Mision("Salva al cazador", "Derrota a los lobos que atacan al cazador", "P-007"));
        seguimiento.Add(new Mision("Busca al Basajaun", "Adentrate en el bosque para encontrar al hombre de los bosques", "P-008"));
        seguimiento.Add(new Mision("Extingue el fuego", "Apaga el fuego de los alrededores del bosque", "P-009"));
        seguimiento.Add(new Mision("Vuelve con Basajaun", "Has terminado con el recado, ya debería tener una respuesta", "P-010"));
        seguimiento.Add(new Mision("Ve a la cima del monte Itzal", "Es hora de pedirle explicaciones a tu hermano", "P-011"));
        seguimiento[misionActual].MisionAceptada();

    }
    public Mision obtenerMisionActual()
    {
        return seguimiento[misionActual];
    }
    public void AvanzarMision(string codigo)
    {
        Debug.Log(seguimiento[misionActual].GetCodigo());
        if(seguimiento[misionActual].GetCodigo() == codigo)
        {
            seguimiento[misionActual].MisionCompletada();
            if(misionActual != seguimiento.Count)
            {
                misionActual++;
                seguimiento[misionActual].MisionAceptada();
            }
        }
        Debug.Log(seguimiento[misionActual].GetNombre());
    }
    public bool GetEstadoMision(string codigo)
    {
        foreach(Mision mision in seguimiento)
        {
            if (mision.GetCodigo() == codigo)
                return mision.GetEstadoMision();
        }
        return false;
    }
    public void ActualizarMision(string codigo)
    {
        foreach (Mision mision in seguimiento)
        {
            if (mision.GetCodigo() == codigo)
                mision.EnemigoMuerto();
        }
    }
    public bool EstanLosRequisitosCompletados(string codigo)
    {
        foreach (Mision mision in seguimiento)
        {
            if (mision.GetCodigo() == codigo)
                return mision.RevisarRequisitos();
        }
        return false;
    }
    public string GetMisionActual()
    {
        return seguimiento[misionActual].GetCodigo();
    }
}
