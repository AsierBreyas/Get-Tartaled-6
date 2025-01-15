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
        seguimiento.Add(new Mision("Ve a la huerta", "Ve a la huerta de amoña a recoger las lechugas y los huevos, seguramente Izaro este alli", "P-000"));
        seguimiento.Add(new Mision("Mata a los jabalís", "Mata a los jabalís que se estan comiendo las cosechas", "P-000"));
        seguimiento.Add(new Mision("Llevale las sobras a amoña y preguntarle por Zaloa", "Entregale la lechuga y el huevo a amoña", "P-000"));
        seguimiento.Add(new Mision("Busca a Zaloa", "Explora el pueblo en busca de Zaloa", "P-000"));
        seguimiento.Add(new Mision("¡Protege la aldea!", "Acaba con los cerdos", "P-000"));
        seguimiento.Add(new Mision("Obten información sobre el ataque", "Los cerdos es raro que ataquen la aldea, alguien tiene que estar detrás de esto", "P-000"));
        seguimiento.Add(new Mision("Busca al Basajaun", "Adentrate en el bosque para encontrar al hombre de los bosques", "P-000"));
        seguimiento.Add(new Mision("Extingue el fuego", "Apaga el fuego de los alrededores del bosque", "P-000"));
        seguimiento.Add(new Mision("Vuelve con Basajaun", "Has terminado con el recado, ya debería tener una respuesta", "P-000"));
        seguimiento.Add(new Mision("Ve a la cima del monte Itzal", "Es hora de pedirle explicaciones a tu hermano", "P-000"));
        seguimiento[misionActual].MisionAceptada();

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
            seguimiento[misionActual].MisionAceptada();
        }
        Debug.Log(seguimiento[misionActual].GetNombre());
    }
}
