using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField]
    float velocidad = 10;
    Vector3 destino;

    // Update is called once per frame
    void Update()
    {
        if (destino != new Vector3(0, 0, 0))
        {
            if(Vector3.Distance(this.transform.position, destino) > 1.5 || Vector3.Distance(this.transform.position, destino) < -1.5)
            Debug.Log(this.transform.position);
            this.transform.position += destino * velocidad * Time.deltaTime;
        }
    }
    public void añadirDestino(Vector3 _destino)
    {
        Debug.Log(_destino);
        destino = _destino;
    }
}
