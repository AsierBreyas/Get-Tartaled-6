[System.Serializable]
public class Mision
{
    string nombre;
    bool completada;
    string descripcion;

    public Mision(string _nombre, string _descripcion)
    {
        nombre = _nombre;
        descripcion = _descripcion;
        completada = false;
    }
    public void MisionCompletada()
    {
        completada = true;
    }
    public bool GetEstadoMision()
    {
        return completada;
    }
}
