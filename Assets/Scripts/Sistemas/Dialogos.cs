using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogos
{
    public bool isResume;
    public bool isTrigger;
    public string misionCode;
    [TextArea(1,10)]
    public List<string> dialgos = new List<string>();
}
