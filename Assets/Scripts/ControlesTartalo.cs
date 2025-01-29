using System.Collections.Generic;
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
    Transform playerRingPos;
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
    Dialogue npcDialogo;
    bool puedeHablar;
    Rigidbody rb;
    bool hayInteractuable;
    GameObject interactuable;
    Enemy enemigoGolpear;


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
    bool heGolpeado;
    bool aturdido;

    //Sistema de vida
    [SerializeField] float maxHealth = 100;
    public float currentHealth;
    [SerializeField] PlayerHealthbar healthbar;
    [SerializeField] float recuperacionComer;

    //Sistema de estamina
    [SerializeField]
    float estaminaMaxima;
    float estaminaActual;
    [SerializeField]
    float gastoEstamina;
    [SerializeField]
    float recuperaEstamina;
    [SerializeField]
    Slider barraEstamina;

    List<GameObject> enemigosCercanos = new List<GameObject>();
    bool hayComestibleCerca;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
        velocidad = velocidadBase;
        mirillaPosOriginal = mirillaPosicion.position;
        ResetMirilla();
        Cursor.visible = false;
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        barraEstamina.maxValue = estaminaMaxima;
        estaminaActual = estaminaMaxima;
        barraEstamina.value = estaminaActual;
        barraEstamina.enabled = false;
    }

    void Update()
    {
        ProcesarEstamina();
        if (!aturdido)
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
        }
        if (!estaEnDefensa)
        {
            if (estaEnAtaqueArea)
                AtaqueArea();
        }
        if (currentHealth <= 0)
        {
            FindFirstObjectByType<GameManager>().ItsGameOver();
        }
    }
    void OnMoverse(InputValue value)
    {
        movimiento = value.Get<Vector3>();
    }
    void OnCorrer(InputValue value)
    {
        estoyCorriendo = value.isPressed;
    }
    public void OnAtaqueNormal(InputValue value)
    {
        if (!aturdido)
        {
            botonDelAtaqueFuerteMantenido = value.isPressed;
            //Debug.Log("PUM! Te pego");
            if (!estaHaciendoMovimiento)
                ProcesarAtaqueNormal();
        }
    }
    void OnAtaqueArea(InputValue value)
    {
        if (!aturdido)
        {
            botonDelAtaqueAreaMantenido = value.isPressed;
            //Debug.Log("AAAAAAAAAAAAAAAAAAA");
            if (!estaHaciendoMovimiento)
                ProcesarAtaqueEnArea();
        }
    }
    void OnDefender(InputValue value)
    {
        if (!estaEnAtaque || !aturdido)
        {
            estaEnDefensa = value.isPressed;
            //Debug.Log("No puedes golpear lo que no puedes ver");
            ProcesarDefensa();
        }
    }
    void OnTirarPiedra(InputValue input)
    {
        if (!aturdido)
        {
            piedraEnMano = input.isPressed;
            if (tenemosPiedra && input.isPressed)
            {
                ProcesarTirada();
            }
        }
    }
    void OnMoverMirillaGamepad(InputValue value)
    {
        movimientoMirilla = value.Get<Vector2>();
        hayMando = true;
    }
    void OnMoverMirillaRaton(InputValue value)
    {
        if (posicionRaton != value.Get<Vector2>())
        {
            posicionRaton = value.Get<Vector2>();
            movimientoMirilla = value.Get<Vector2>();
            hayMando = false;
        }
    }
    void OnInteractuar()
    {
        if (puedeHablar)
        {
            npcDialogo.interactButtonPulsed();
            puedeHablar = false;
        }
        else if (hayComestibleCerca)
            ComerEnemigo();
        else if (hayInteractuable)
            hayInteractuable = FindAnyObjectByType<InteractuableManager>().ActivarInteractuable(interactuable.GetComponent<Interactuable>().GetNombre(), interactuable);

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
        if (estoyCorriendo && (xOffSet !=0 || zOffSet != 0 ))
        {
            estaminaActual -= gastoEstamina * 0.1f;
            ActualizarBarraEstamina();
        }
        //Vector3 direccionMovimiento = new Vector3(playerRingPos.localPosition.x + xOffSet, playerRingPos.localPosition.y, playerRingPos.localPosition.z + zOffSet);
        Vector3 direccionMovimientoNueva = new Vector3(xOffSet, 0f, zOffSet);
        direccionMovimientoNueva.y = 0f;
        rb.linearVelocity = direccionMovimientoNueva;
        if (direccionMovimientoNueva.magnitude > 0.1f)
        {
            //var relative = (transform.position + direccionMovimientoNueva) - transform.position;
            var rot = Quaternion.LookRotation(direccionMovimientoNueva);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, velocidadRotacion * Time.deltaTime);
        }
        //transform.rotation = rot;
        //Quaternion rotacion = Quaternion.LookRotation(direccionMovimiento);
        //rotacion = Quaternion.RotateTowards(transform.rotation, rotacion, 360 * Time.fixedDeltaTime);
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, rotacion, velocidadRotacion);
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
        if (!aturdido)
        {
            estaminaActual -= gastoEstamina;
            ActualizarBarraEstamina();
            if (!aturdido)
            {
                estaEnAtaque = true;
                estaEnAtaqueNormal = true;
                estaHaciendoMovimiento = true;
                Arma.transform.Rotate(new Vector3(-75, 0, 0));
            }
        }
    }
    void ProcesarGolpeFuerte()
    {
        if (!aturdido)
        {
            estaminaActual -= gastoEstamina * 3;
            ActualizarBarraEstamina();
            if (!aturdido)
            {
                estaEnAtaque = true;
                estaEnAtaqueFuerte = true;
                estaHaciendoMovimiento = true;
                Arma.transform.Rotate(new Vector3(-75, 0, 0));
            }
        }
        //Debug.Log("MADA MADA");
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
        if (contadorMovimientoDefensa == 0)
        {
            Arma.transform.Rotate(new Vector3(0, 90, 0));
            contadorMovimientoDefensa++;
        }

    }
    void ProcesarTirada()
    {
        estaminaActual -= gastoEstamina * 2;
        ActualizarBarraEstamina();
        if (!aturdido)
        {
            estaHaciendoMovimiento = true;
            estaEnAtaque = true;
            estaTirandoPiedra = true;
            piedra.transform.position = posicionOtraMano.position;
        }
    }
    void ProcesarEstamina()
    {
        EstoyAturdido();
        if (!estaHaciendoMovimiento && estaminaActual <= estaminaMaxima)
        {
            if (movimiento == Vector3.zero)
                estaminaActual += recuperaEstamina * 2;
            else
                estaminaActual += recuperaEstamina;
            ActualizarBarraEstamina();
            if (estaminaActual > estaminaMaxima / 4)
                aturdido = false;
        }
        else if (estaminaActual >= estaminaMaxima)
        {
            estaminaActual = 100;
            barraEstamina.enabled = false;
        }
    }
    void GolpeNormal()
    {
        //Debug.Log("Rotacion de x: "+ Arma.transform.rotation.eulerAngles);
        if (Arma.transform.rotation.eulerAngles.x >= 70f && Arma.transform.rotation.eulerAngles.x <= 75f)
        {
            if (heGolpeado)
            {
                ProcesarDañosHechos();
                //Damages
                heGolpeado = false;
            }
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
            if (heGolpeado)
            {
                ProcesarDañosHechos();
                //Damages
                heGolpeado = false;
            }
            estaEnAtaque = false;
            estaEnAtaqueFuerte = false;
            estaHaciendoMovimiento = false;
            Arma.transform.Rotate(new Vector3(-75, 0, 0));
            if (botonDelAtaqueFuerteMantenido && !aturdido)
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
            estaminaActual -= gastoEstamina * 0.2f;
            ActualizarBarraEstamina();
            //Debug.Log("Dalta Faño");
        }
        if (!botonDelAtaqueAreaMantenido || aturdido)
        {
            //Debug.Log("Rotacion de x: " + Arma.transform.rotation.eulerAngles.y);
            if (Arma.transform.rotation.eulerAngles.y >= 90f && Arma.transform.rotation.eulerAngles.y <= 92f)
            {
                if (heGolpeado)
                {
                    ProcesarDañosHechos();
                    //Damages
                    heGolpeado = false;
                }
                //Debug.Log("Ya no me sale :(");
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
            //Debug.Log("No more defensa");
            estaHaciendoMovimiento = false;
            if (contadorMovimientoDefensa == 1)
            {
                Arma.transform.Rotate(new Vector3(0, -90, 0));
                contadorMovimientoDefensa--;
            }
        }
        else if (estaHaciendoMovimiento && !estaEnAtaque)
        {
            //Debug.Log(">:D");
        }
    }
    void TirarPiedra()
    {
        contadorPiedra += +Time.deltaTime * 1.5f;
        if (!piedraEnMano || aturdido)
        {
            //Debug.Log(mirillaPosicion.position);
            boloncho.position = Camera.main.ScreenToWorldPoint(new Vector3(mirillaPosicion.position.x, mirillaPosicion.position.y, targetDistance));
            piedra.GetComponent<Proyectil>().añadirDestino(boloncho.position);
            estaHaciendoMovimiento = false;
            estaEnAtaque = false;
            estaTirandoPiedra = false;
            ResetMirilla();
        }
        else
        {
            if (contadorPiedra > 1)
            {
                //Debug.Log("Empezo mi tirania");
                if (hayMando)
                    trasladoMirilla = new Vector2(mirillaPosicion.position.x + movimientoMirilla.x, mirillaPosicion.position.y + movimientoMirilla.y);
                else
                    trasladoMirilla = movimientoMirilla;
                mirillaPosicion.position = trasladoMirilla;
                estaminaActual -= gastoEstamina * 0.2f;
                ActualizarBarraEstamina();
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
        if (other.tag == "Roca" && !tenemosPiedra)
        {
            piedra = other.gameObject;
            tenemosPiedra = true;
        }
        else if (other.tag == "NPC")
        {
            npcDialogo = other.gameObject.GetComponent<Dialogue>();
            puedeHablar = true;
        }
        else if (other.gameObject.layer == 7 && other.tag == "Interactuable")
        {
            Debug.Log("OMG HIIIII");
            enemigosCercanos.Add(other.transform.parent.gameObject);
        }
        else if (other.tag == "Interactuable")
        {
            hayInteractuable = true;
            interactuable = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Troste");
        if (other.tag == "Roca" && tenemosPiedra)
        {
            piedra = null;
            tenemosPiedra = false;
        }
        else if (other.tag == "NPC")
        {
            npcDialogo = null;
            puedeHablar = false;
        }
        else if (other.gameObject.layer == 7)
        {
            enemigosCercanos.Remove(other.gameObject);
        }
        else if (other.tag == "Interactuable")
        {
            hayInteractuable = false;
            interactuable = null;
        }
    }
    public void puedeSeguirHablando()
    {
        puedeHablar = true;
    }

    public void TakeDamage(float damage)
    {
        if (estaEnDefensa)
        {
            currentHealth -= damage * 0.5f;
            estaminaActual -= gastoEstamina * 3;
            ActualizarBarraEstamina();
        }
        else if (aturdido)
            currentHealth -= damage * 2f;
        else
            currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }
    public void HeGolpeado(Enemy enemigo)
    {
        heGolpeado = true;
        enemigoGolpear = enemigo;
        Debug.Log("PUM! TE HOSTIO");
        
    }
    void ProcesarDañosHechos()
    {
        if (enemigoGolpear != null)
        {
            if (estaEnAtaqueNormal)
            {
                enemigoGolpear.TakeDamage(15f);
            }
            else if (estaEnAtaqueFuerte)
            {
                enemigoGolpear.TakeDamage(30f);
            }
            else if (estaEnAtaqueArea)
            {
                enemigoGolpear.TakeDamage(15f);
            }
        }
        enemigoGolpear = null;
    }
    void ActualizarBarraEstamina()
    {
        if (estaminaActual != estaminaMaxima)
            barraEstamina.enabled = true;
        else
            barraEstamina.enabled = false;
        barraEstamina.value = estaminaActual;
        EstoyAturdido();
    }
    void EstoyAturdido()
    {
        if (estaminaActual < 0)
        {
            Debug.Log("Me aturdi soy inutil");
            aturdido = true;
            estaminaActual = 0;
        }
    }
    public void AparecioComestible()
    {
        hayComestibleCerca = true;
    }
    void ComerEnemigo()
    {
        bool yaHeComido = false;
        foreach(GameObject enemigo in enemigosCercanos)
        {
            Enemy enemigoSc = enemigo.GetComponent<Enemy>();
            Debug.Log(enemigo.GetComponent<Enemy>().GetIsEsdible());
            if (enemigoSc.GetIsEsdible() && enemigoSc.IsDead() && !yaHeComido)
            {
                yaHeComido = true;
                hayComestibleCerca = false;
                enemigoSc.BeEat();
                enemigosCercanos.Remove(enemigo);
                RecuperarVida(recuperacionComer);
                Debug.Log("NOM NOM NOM");
            }
            else if(enemigoSc.GetIsEsdible() && enemigoSc.IsDead() && yaHeComido)
            {
                hayComestibleCerca = true;
                Debug.Log("Bueno si no gomito");
            }
        }
    }
    void RecuperarVida(float recuperacion)
    {
        if (currentHealth + recuperacion > 100)
            currentHealth = 100;
        else
            currentHealth += recuperacion;
        healthbar.SetHealth(currentHealth);
    }
}
