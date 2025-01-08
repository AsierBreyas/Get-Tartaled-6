using UnityEngine;
using UnityEngine.InputSystem;

public class ControlesTartalo : MonoBehaviour
{
    Vector3 movimiento;
    [SerializeField]
    float velocidadBase = 10f;
    float velocidad;
    bool estoyCorriendo;
    [SerializeField]
    float velocidadRotacion = 10f;
    [SerializeField]
    GameObject Arma;

    //Booleanos para los ataques
    bool estaEnAtaque;
    bool estaEnAtaqueNormal;
    bool estaEnAtaqueFuerte;
    bool botonDelAtaqueFuerteMantenido;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocidad = velocidadBase;
    }

    // Update is called once per frame
    void Update()
    {
     ProcesarVelocidad();
     ProcesarMovimiento();
     if (estaEnAtaqueNormal)
        GolpeNormal();
     if (estaEnAtaqueFuerte)
        GolpeFuerte();
    }
    void OnMoverse(InputValue value)
    {
        movimiento = value.Get<Vector3>();
    }
    void OnCorrer(InputValue value)
    {
        estoyCorriendo = value.isPressed;
    }
    void OnAtaqueNormal(InputValue value)
    {
        botonDelAtaqueFuerteMantenido = value.isPressed;
        Debug.Log("PUM! Te pego");
        if(!estaEnAtaque)
            ProcesarAtaqueNormal();
    }
    void ProcesarMovimiento()
    {
        float xOffSet = movimiento.x * velocidad * Time.deltaTime;
        float zOffSet = movimiento.z * velocidad * Time.deltaTime;
        Vector3 direccionMovimiento = new Vector3(transform.localPosition.x + xOffSet, transform.localPosition.y, transform.localPosition.z + zOffSet);
        transform.localPosition = direccionMovimiento;
        Quaternion rotacion = Quaternion.LookRotation(direccionMovimiento);
        rotacion = Quaternion.RotateTowards(transform.rotation, rotacion, 360 * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotacion, velocidadRotacion);
    }
    void ProcesarVelocidad()
    {
        if (estoyCorriendo)
            velocidad = velocidadBase * 2;
            //Debug.Log("Soy uno con el viento wiiiii");
        else
            velocidad = velocidadBase;
    }
    void ProcesarAtaqueNormal()
    {
        estaEnAtaque = true;
        estaEnAtaqueNormal = true;
        Arma.transform.Rotate(new Vector3(-75, 0, 0));
    }
    void ProcesarGolpeFuerte()
    {
        estaEnAtaque = true;
        estaEnAtaqueFuerte = true;
        Arma.transform.Rotate(new Vector3(-75, 0, 0));
        Debug.Log("MADA MADA");
    }
    void GolpeNormal()
    {
        //Debug.Log("Rotacion de x: "+ Arma.transform.rotation.eulerAngles);
        if (Arma.transform.rotation.eulerAngles.x >= 70f && Arma.transform.rotation.eulerAngles.x <= 75f)
        {
            estaEnAtaque = false;
            estaEnAtaqueNormal = false;
            Arma.transform.Rotate(new Vector3(-75, 0, 0));
            if (botonDelAtaqueFuerteMantenido)
                ProcesarGolpeFuerte();
        }
        else
        {
            Arma.transform.Rotate(new Vector3(75, 0, 0) * 3 * Time.deltaTime);
        }
    }
    void GolpeFuerte()
    {
        if (Arma.transform.rotation.eulerAngles.x >= 70f && Arma.transform.rotation.eulerAngles.x <= 75f)
        {
            estaEnAtaque = false;
            estaEnAtaqueFuerte = false;
            Arma.transform.Rotate(new Vector3(-75, 0, 0));
            if (botonDelAtaqueFuerteMantenido)
                ProcesarGolpeFuerte();
        }
        else
        {
            Arma.transform.Rotate(new Vector3(75, 0, 0) * 6 * Time.deltaTime);
        }
    }
}
