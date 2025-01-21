using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    string codigoMision;
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

        }
    }
}
