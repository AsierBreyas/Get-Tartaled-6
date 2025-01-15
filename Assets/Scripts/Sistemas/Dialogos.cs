using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogos
{
    public bool isResume;
    public bool isTrigger;
    public string misionCode;
    public string hablador;
    public List<Dialogo> dialgos = new List<Dialogo>();
}
