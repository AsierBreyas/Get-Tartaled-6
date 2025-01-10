using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField]
    float velocidad = 10;
    Vector3 destino;

    // Update is called once per frame
    void Update()
    {
        if (destino != null)
        {
            this.transform.position = destino * velocidad * Time.deltaTime;
        }
    }
    public void añadirDestino(Vector3 _destino)
    {
        destino = _destino;
    }
}
