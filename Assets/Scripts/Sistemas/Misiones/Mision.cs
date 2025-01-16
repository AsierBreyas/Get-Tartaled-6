[System.Serializable]
public class Mision
{
    string nombre;
    bool completada;
    bool aceptada;
    string descripcion;
    string codigo;
    bool requisitosCompletados;
    int enemigosNecesarios;
    int enemigosActuales;

    public Mision(string _nombre, string _descripcion, string _codigo)
    {
        nombre = _nombre;
        descripcion = _descripcion;
        codigo = _codigo;
        requisitosCompletados = true;
    }
    public void MisionCompletada()
    {
        if(requisitosCompletados)
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
    public string GetNombre()
    {
        return nombre;
    }
    public string GetCodigo()
    {
        return codigo;
    }
    public string GetDescripcion()
    {
        return descripcion;
    }
    public void SetEnemigosAMatar(int numeroEnemigos, int actuales)
    {
        requisitosCompletados = false;
        enemigosNecesarios = numeroEnemigos;
        enemigosActuales = actuales;
    }
    public void EnemigoMuerto()
    {
        enemigosActuales++;
        if (enemigosActuales == enemigosNecesarios)
            requisitosCompletados = true;
    }
}
