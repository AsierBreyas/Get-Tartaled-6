using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // Datos a guardar
    public float currentHealth;
    public float[] position;
    // Misiones aqui

    public PlayerData (ControlesTartalo controlesTartalo)
    {
        currentHealth = controlesTartalo.currentHealth;
        position = new float[3];
        position[0] = controlesTartalo.transform.position.x;
        position[1] = controlesTartalo.transform.position.y;
        position[2] = controlesTartalo.transform.position.z;
    }
}
