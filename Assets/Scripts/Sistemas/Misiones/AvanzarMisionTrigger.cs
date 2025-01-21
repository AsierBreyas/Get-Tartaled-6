using UnityEngine;

public class AvanzarMisionTrigger : MonoBehaviour
{
    [SerializeField]
    string codigoMision;
    MisionManager misionManager;

    [SerializeField]
    bool spawneaCosas;
    [SerializeField]
    GameObject cosaQueSpawnear;
    [SerializeField]
    Transform[] lugaresDondeSpawnear;
    bool cague;

    private void Start()
    {
        misionManager = FindAnyObjectByType<MisionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //&& FindAnyObjectByType<SeguimientoMisionPrincipal>().obtenerMisionActual().GetCodigo() == codigoMision
        if (other.tag == "Player")
        {
            misionManager.AvanzarMision(codigoMision);
            if (misionManager.GetEstadoMision(codigoMision) && spawneaCosas)
            {
                SpawnearCosas();
            }
        }
    }
    private void SpawnearCosas()
    {
        if (!cague)
        {
        foreach (Transform sitio in lugaresDondeSpawnear)
            {
                Instantiate(cosaQueSpawnear,sitio);
                cague = true;
            }
        }
    }

}
