using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] 
    GameObject elOtroLugar;
    [SerializeField]
    string misionRequerida;
    bool puedePasar;
    bool estoyTepeando;
    Teleport elOtroTeleport;

    private void Start()
    {
        elOtroTeleport = elOtroLugar.GetComponent<Teleport>();
        if (misionRequerida != "")
            puedePasar = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(misionRequerida != "" && !puedePasar)
        {
            puedePasar = FindAnyObjectByType<MisionManager>().EstaAceptadaLaMision(misionRequerida);
        }
        if(other.tag == "Player" && !elOtroTeleport.EstoyTepeando() && puedePasar)
        {
            other.transform.position = elOtroLugar.transform.position;
            estoyTepeando = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            elOtroTeleport.TerminarTeleport();
    }
    public bool EstoyTepeando()
    {
        return estoyTepeando;
    }
    public void TerminarTeleport()
    {
        estoyTepeando = false;
    }
}
