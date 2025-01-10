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
    bool didDialogueStart;
    int lineIndex;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Fire1"))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
        }
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

    void StartDialogue()
    {
        didDialogueStart = true;
    }
}
