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
        }
    }
    public void LlenarCubo()
    {
        estaLleno = true;
    }
}
