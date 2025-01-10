using UnityEngine;
using System.Collections;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] GameObject dialogueMark;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] string[] dialogueLines;
    bool isPlayerInRange;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInRange = true;
        dialogueMark.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerInRange = false;
        dialogueMark.SetActive(false);
    }
}
