using UnityEngine;

public class InteractuableManager : MonoBehaviour
{
    bool estaCogidoCubo;
    Cubo cubo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubo = GetComponent<Cubo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ActivarInteractuable(string interactuable, GameObject objeto)
    {
        switch (interactuable)
        {
            case ("Cubo"):
                estaCogidoCubo = true;
                Debug.Log("TENGO UN CUBO");
                Destroy(objeto);
                return false;
            case ("Agua"):
                RellenarCubo();
                return true;
            case ("Fuego"):
                cubo.ApagarFuego(objeto);
                return false;
            default:
                return false;
        }
    }
    void RellenarCubo()
    {
        Debug.Log("GLU GLU GLU");
        if (estaCogidoCubo)
            cubo.LlenarCubo();
    }
}
