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
        if (misionManager.RevisarRequisitos(codigoMision))
        {
            misionManager.AvanzarMision(codigoMision);
            Debug.Log("Jejejej doy pasitos");
        }
    }
}
