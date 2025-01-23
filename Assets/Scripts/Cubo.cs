using UnityEngine;

public class Cubo : MonoBehaviour
{
    bool estaLleno;
    public void ApagarFuego(GameObject fuego)
    {
        if (estaLleno)
        {
            Debug.Log("Fuego se quedo sin amigos");
            estaLleno = false;
            Destroy(fuego);
        }
    }
    public void LlenarCubo()
    {
        estaLleno = true;
    }
}
