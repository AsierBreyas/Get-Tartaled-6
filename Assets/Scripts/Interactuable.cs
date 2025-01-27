using UnityEngine;

public class Interactuable : MonoBehaviour
{
    [SerializeField]
    string nombre;
    [SerializeField]
    string codigoMision;

    public string GetNombre()
    {
        return nombre;
    }
    public bool EsDeMision()
    {
        return codigoMision != "";
    }
    public string GetCodigoMision()
    {
        return codigoMision;
    }
}
