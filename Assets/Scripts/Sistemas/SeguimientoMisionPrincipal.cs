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
        seguimiento.Add(new Mision("Hablar con Izaro", "Ve al pueblo a contarle lo que has so�ado a Izaro"));
        seguimiento.Add(new Mision("Ve a la huerta", "Ve a la huerta de amo�a a recoger las lechugas y los huevos, seguramente Izaro este alli"));
        seguimiento.Add(new Mision("Mata a los jabal�s", "Mata a los jabal�s que se estan comiendo las cosechas"));
        seguimiento.Add(new Mision("Llevale las sobras a amo�a y preguntarle por Zaloa", "Entregale la lechuga y el huevo a amo�a"));
        seguimiento.Add(new Mision("Busca a Zaloa", "Explora el pueblo en busca de Zaloa"));
        seguimiento.Add(new Mision("�Protege la aldea!", "Acaba con los cerdos"));
        seguimiento.Add(new Mision("Obten informaci�n sobre el ataque", "Los cerdos es raro que ataquen la aldea, alguien tiene que estar detr�s de esto"));
        seguimiento.Add(new Mision("Busca al Basajaun", "Adentrate en el bosque para encontrar al hombre de los bosques"));
        seguimiento.Add(new Mision("Extingue el fuego", "Apaga el fuego de los alrededores del bosque"));
        seguimiento.Add(new Mision("Vuelve con Basajaun", "Has terminado con el recado, ya deber�a tener una respuesta"));
        seguimiento.Add(new Mision("Ve a la cima del monte Itzal", "Es hora de pedirle explicaciones a tu hermano"));
    }
    public Mision obtenerMisionActual()
    {
        return seguimiento[misionActual];
    }
    public void AvanzarMision()
    {
        seguimiento[misionActual].MisionCompletada();
        if(misionActual != seguimiento.Count)
        {
            misionActual++;
        }
    }
}
