public class ProfileData
{
    // Datos perfil
    public string fileName;
    public string name;
    public bool newGame;

    // Datos a guardar
    public float currentHealth;
    public float x, y, z;

    // Misiones aqui

    public ProfileData()
    {
        this.fileName = "None.xml";
        this.name = "None";
        this.newGame = false;

        this.y = this.x = this.z = 0;
    }
    
    public ProfileData (string name, bool newGame, float x, float y, float z, float currentHealth)
    {
        this.fileName = name.Replace(" ", "_") + ".xml";
        this.name = name;
        this.newGame = newGame;
        this.x = x;
        this.y = y;
        this.z = z;
        this.currentHealth = currentHealth;
    }
}
