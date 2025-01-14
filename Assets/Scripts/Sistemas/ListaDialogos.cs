using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListaDialogos
{
    [TextArea(1,10)]
    public List<string> dialgos = new List<string>();
}
