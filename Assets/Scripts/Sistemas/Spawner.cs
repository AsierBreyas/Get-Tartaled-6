using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    string codigoMision;
    [SerializeField]
    GameObject objetoSpawnear;
    [SerializeField]
    Transform[] lugaresSpawnear;
    MisionManager misionManager;
    bool cague;

    private void Start()
    {
        misionManager = FindAnyObjectByType<MisionManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (misionManager.GetMisionPrincipalActual() == codigoMision && !cague)
        {
            foreach(Transform lugar in lugaresSpawnear)
            {
                Instantiate(objetoSpawnear, lugar);
            }
            cague = true;
        }
    }
}
