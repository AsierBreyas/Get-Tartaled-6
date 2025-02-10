using UnityEngine;

public class TriggerTP : MonoBehaviour
{
    [SerializeField]
    GameObject teleport;
    bool estoyTepeando;
    [SerializeField]
    string misionNecesaria;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !teleport.GetComponent<TriggerTP>().estaTepeando() && FindAnyObjectByType<MisionManager>().EstaAceptadaLaMision(misionNecesaria))
        {
            estoyTepeando = true;
            //poner pantalla de carga
            other.gameObject.transform.position = teleport.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && teleport.GetComponent<TriggerTP>().estaTepeando())
            teleport.GetComponent<TriggerTP>().yaNoTepea();
    }
    public bool estaTepeando()
    {
        return estoyTepeando;
    }
    public void yaNoTepea()
    {
        estoyTepeando = false;
    }
}
