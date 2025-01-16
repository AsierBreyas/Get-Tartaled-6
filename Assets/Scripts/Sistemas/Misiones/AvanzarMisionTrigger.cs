using UnityEngine;

public class AvanzarMisionTrigger : MonoBehaviour
{
    [SerializeField]
    string codigoMision;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && FindAnyObjectByType<SeguimientoMisionPrincipal>().obtenerMisionActual().GetCodigo() == codigoMision)
        {

        }
    }
}
