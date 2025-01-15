using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlesTartalo : MonoBehaviour
{
    Vector3 movimiento;
    Vector2 movimientoMirilla;
    Vector2 posicionRaton;
    [SerializeField]
    float velocidadBase = 10f;
    float velocidad;
    bool estoyCorriendo;
    [SerializeField]
    float velocidadRotacion = 10f;
    [SerializeField]
    GameObject Arma;
    [SerializeField]
    Transform posicionOtraMano;
    int contadorMovimientoDefensa;
    GameObject piedra;
    float contadorPiedra;
    [SerializeField]
    Transform boloncho;
    [SerializeField]
    float targetDistance = 100f;
    [SerializeField]
    Image mirilla;
    [SerializeField]
    RectTransform mirillaPosicion;
    Vector2 mirillaPosOriginal;
    bool hayMando;
    Vector2 trasladoMirilla;


    //Booleanos para los ataques
    bool estaHaciendoMovimiento;
    bool estaEnAtaque;
    bool estaEnAtaqueNormal;
    bool estaEnAtaqueFuerte;
    bool botonDelAtaqueFuerteMantenido;
    bool botonDelAtaqueAreaMantenido;
    bool estaEnAtaqueArea;
    bool estaEnDefensa;
    bool tenemosPiedra;
    bool piedraEnMano;
    bool estaTirandoPiedra;

    //Sistema de vida
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth;
    [SerializeField] PlayerHealthbar healthbar;
    void Start()
    {
        velocidad = velocidadBase;
        mirillaPosOriginal = mirillaPosicion.position;
        ResetMirilla();
        Cursor.visible = false;
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (!estaEnDefensa)
        {
            ProcesarVelocidad();
            ProcesarMovimiento();
            if (estaEnAtaqueNormal)
                GolpeNormal();
            else if (estaEnAtaqueFuerte)
                GolpeFuerte();
            else if (estaEnAtaqueArea)
                AtaqueArea();
            else if (estaTirandoPiedra)
                TirarPiedra();
        }
        Defensa();
        //Solo para probar, quitar esto antes de exportar
        QuitarVidaDebug();
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
        if(!estaHaciendoMovimiento)
            ProcesarAtaqueNormal();
    }
    void OnAtaqueArea(InputValue value)
    {
        botonDelAtaqueAreaMantenido = value.isPressed;
        Debug.Log("AAAAAAAAAAAAAAAAAAA");
        if (!estaHaciendoMovimiento)
            ProcesarAtaqueEnArea();
    }
    void OnDefender(InputValue value)
    {
        if (!estaEnAtaque)
        {
            estaEnDefensa = value.isPressed;
            Debug.Log("No puedes golpear lo que no puedes ver");
            ProcesarDefensa();
        }
    }
    void OnTirarPiedra(InputValue input)
    {
        piedraEnMano = input.isPressed;
        if (tenemosPiedra && input.isPressed)
        {
            ProcesarTirada();
        }
    }
    void OnMoverMirillaGamepad(InputValue value)
    {
        movimientoMirilla = value.Get<Vector2>();
        hayMando = true;
    }
    void OnMoverMirillaRaton(InputValue value)
    {
        if(posicionRaton != value.Get<Vector2>())
        {
            posicionRaton = value.Get<Vector2>();
            movimientoMirilla = value.Get<Vector2>();
            hayMando = false;
        }
    }
    void ProcesarMovimiento()
    {
        float xOffSet = movimiento.x * velocidad * Time.deltaTime;
        float zOffSet = movimiento.z * velocidad * Time.deltaTime;
        if (estaEnAtaque && !estoyCorriendo)
        {
            zOffSet /= 2;
            xOffSet /= 2;
        }
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
        estaHaciendoMovimiento = true;
        Arma.transform.Rotate(new Vector3(-75, 0, 0));
    }
    void ProcesarGolpeFuerte()
    {
        estaEnAtaque = true;
        estaEnAtaqueFuerte = true;
        estaHaciendoMovimiento = true;
        Arma.transform.Rotate(new Vector3(-75, 0, 0));
        Debug.Log("MADA MADA");
    }
    void ProcesarAtaqueEnArea()
    {
        estaEnAtaque = true;
        estaEnAtaqueArea = true;
        estaHaciendoMovimiento = true;
        Arma.transform.Rotate(new Vector3(0, -60, 0));

    }
    void ProcesarDefensa()
    {
        estaHaciendoMovimiento = true;
        if(contadorMovimientoDefensa == 0)
        {
            Arma.transform.Rotate(new Vector3(0, 90, 0));
            contadorMovimientoDefensa++;
        }

    }
    void ProcesarTirada()
    {
        estaHaciendoMovimiento = true;
        estaEnAtaque = true;
        estaTirandoPiedra = true;
        piedra.transform.position = posicionOtraMano.position;

    }
    void GolpeNormal()
    {
        //Debug.Log("Rotacion de x: "+ Arma.transform.rotation.eulerAngles);
        if (Arma.transform.rotation.eulerAngles.x >= 70f && Arma.transform.rotation.eulerAngles.x <= 75f)
        {
            estaEnAtaque = false;
            estaEnAtaqueNormal = false;
            estaHaciendoMovimiento = false;
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
            estaHaciendoMovimiento = false;
            Arma.transform.Rotate(new Vector3(-75, 0, 0));
            if (botonDelAtaqueFuerteMantenido)
                ProcesarGolpeFuerte();
        }
        else
        {
            Arma.transform.Rotate(new Vector3(75, 0, 0) * 6 * Time.deltaTime);
        }
    }
    void AtaqueArea()
    {
        if (botonDelAtaqueAreaMantenido)
        {
            Debug.Log("Dalta Fa�o");
        }
        else
        {
            //Debug.Log("Rotacion de x: " + Arma.transform.rotation.eulerAngles.y);
            if (Arma.transform.rotation.eulerAngles.y >= 90f && Arma.transform.rotation.eulerAngles.y <= 92f)
            {
                Debug.Log("Ya no me sale :(");
                estaEnAtaque = false;
                estaEnAtaqueArea = false;
                estaHaciendoMovimiento = false;
                Arma.transform.Rotate(new Vector3(0, -90, 0));
            }
            else
            {
                Arma.transform.Rotate(new Vector3(0, 60, 0) * 10 * Time.deltaTime);
            }
        }
    }
    void Defensa()
    {
        if (!estaEnDefensa && estaHaciendoMovimiento && !estaEnAtaque)
        {
            Debug.Log("No more defensa");
            estaHaciendoMovimiento = false;
            if(contadorMovimientoDefensa == 1)
            {
                Arma.transform.Rotate(new Vector3(0, -90, 0));
                contadorMovimientoDefensa--;
            }
        }
        else if(estaHaciendoMovimiento && !estaEnAtaque)
        {
            Debug.Log(">:D");
        }
    }
    void TirarPiedra()
    {
        contadorPiedra += +Time.deltaTime * 1.5f;
        if (!piedraEnMano)
        {
            Debug.Log(mirillaPosicion.position);
            boloncho.position = Camera.main.ScreenToWorldPoint(new Vector3(mirillaPosicion.position.x, mirillaPosicion.position.y, targetDistance));
            piedra.GetComponent<Proyectil>().a�adirDestino(boloncho.position);
            estaHaciendoMovimiento = false;
            estaEnAtaque = false;
            estaTirandoPiedra = false;
            ResetMirilla();
        }
        else
        {
            if(contadorPiedra > 1)
            {
                Debug.Log("Empezo mi tirania");
                if (hayMando)
                    trasladoMirilla = new Vector2(mirillaPosicion.position.x + movimientoMirilla.x, mirillaPosicion.position.y + movimientoMirilla.y);
                else
                    trasladoMirilla = movimientoMirilla;
                mirillaPosicion.position = trasladoMirilla;
                mirilla.enabled = true;
            }
        }
    }
    void ResetMirilla()
    {
        mirilla.enabled = false;
        mirillaPosicion.position = mirillaPosOriginal;
        boloncho.position = Camera.main.ScreenToWorldPoint(new Vector3(mirillaPosicion.position.x, mirillaPosicion.position.y, targetDistance));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Roca" && !tenemosPiedra)
        {
            Debug.Log("OMG HIIIII");
            piedra = other.gameObject;
            tenemosPiedra = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Roca" && tenemosPiedra)
        {
            Debug.Log("Troste");
            piedra = null;
            tenemosPiedra = false;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    void QuitarVidaDebug()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }
}
