using UnityEngine;

public class Cubo : MonoBehaviour
{
    bool estaLleno;
    public void ApagarFuego()
    {
        //Jejejej apago el fuego
        estaLleno = false;
    }
    public void LlenarCubo()
    {
        estaLleno = true;
    }
    public bool EstaLleno()
    {
        return estaLleno;
    }
}
