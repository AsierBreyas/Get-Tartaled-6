using UnityEngine;

public class Interactuable : MonoBehaviour
{
    [SerializeField]
    string nombre;

    public string GetNombre()
    {
        return nombre;
    }
}
