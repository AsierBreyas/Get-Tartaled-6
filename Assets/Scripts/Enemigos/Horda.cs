using UnityEngine;

public class Horda : MonoBehaviour
{
    [SerializeField]
    string codigoMision;
    MisionManager misionManager;

    private void Start()
    {
        misionManager = FindAnyObjectByType<MisionManager>();
    }
    public void EnemigoMuerto()
    {
        misionManager.ActualizarEstadoMision(codigoMision);
        Debug.Log("No hay mejor CC que la muerte");
        if (misionManager.RevisarRequisitos(codigoMision))
        {
            misionManager.AvanzarMision(codigoMision);
            Debug.Log("Jejejej doy pasitos");
        }
    }
}
