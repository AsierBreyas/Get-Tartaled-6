using UnityEngine;

public class Cubo : MonoBehaviour
{
    bool estaLleno;
    public void ApagarFuego(GameObject fuego)
    {
        if (estaLleno)
        {
            estaLleno = false;
            Destroy(fuego);
            Interactuable intFuego = fuego.GetComponent<Interactuable>();
            if (intFuego.EsDeMision())
            {
                Debug.Log("Fuego se quedo sin amigos");
                MisionManager misionManager = FindAnyObjectByType<MisionManager>();
                misionManager.ActualizarEstadoMision(intFuego.GetCodigoMision());
                if (misionManager.RevisarRequisitos(intFuego.GetCodigoMision()))
                    misionManager.AvanzarMision(intFuego.GetCodigoMision());
            }
        }
    }
    public void LlenarCubo()
    {
        estaLleno = true;
    }
}
