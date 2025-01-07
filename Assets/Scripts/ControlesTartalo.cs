using UnityEngine;
using UnityEngine.InputSystem;

public class ControlesTartalo : MonoBehaviour
{
    Vector3 movimiento;
    [SerializeField]
    float velocidadBase = 10f;
    float velocidad;
    [SerializeField]
    float velocidadRotacion = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocidad = velocidadBase;
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
    }
    void OnMoverse(InputValue value)
    {
        movimiento = value.Get<Vector3>();
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

        //Quaternion toRotation = Quaternion.Euler(-movimiento.z * velocidad, transform.localRotation.y, -movimiento.x * velocidad);
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, toRotation, velocidadRotacion * Time.deltaTime);
    }
}
