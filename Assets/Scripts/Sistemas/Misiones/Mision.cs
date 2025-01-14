[System.Serializable]
public class Mision
{
    string nombre;
    bool completada;
    bool aceptada;
    string descripcion;
    string codigo;

    public Mision(string _nombre, string _descripcion, string _codigo)
    {
        nombre = _nombre;
        descripcion = _descripcion;
        codigo = _codigo;
    }
    public void MisionCompletada()
    {
        completada = true;
    }
    public void MisionAceptada()
    {
        aceptada = true;
    }
    public bool GetEstadoMision()
    {
        return completada;
    }
    public bool EstaAcepatdaLaMision()
    {
        return aceptada;
    }
}
