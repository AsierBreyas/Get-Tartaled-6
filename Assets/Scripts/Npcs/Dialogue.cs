using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] GameObject dialogueMark;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text speakerNameText;
    [SerializeField] Dialogos[] listOfDialogues;
    Dialogos dialogueLines;
    bool isPlayerInRange;
    bool didDialogueStart;
    bool playerPulsedBoton;
    int lineIndex;
    int currentDialogue;
    float typingTime = 0.05f;

    void Start()
    {
        dialogueLines = listOfDialogues[currentDialogue];
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && playerPulsedBoton)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines.dialgos[lineIndex].texto)
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines.dialgos[lineIndex].texto;
            }
            FindAnyObjectByType<ControlesTartalo>().puedeSeguirHablando();
            playerPulsedBoton = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }

    void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        SetSpeakerName();

        foreach (char ch in dialogueLines.dialgos[lineIndex].texto)
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.dialgos.Count)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;
            if (dialogueLines.isTrigger)
                FindAnyObjectByType<MisionManager>().AvanzarMision(dialogueLines.misionCode);
            if (dialogueLines.isResume == false)
            {
                Debug.Log("MIRA COMO LA POCA SALUD MENTAL QUE ME QUEDA SE ESTA TIRANDO POR LA TXIRRISTRA DEL TXIKIPARK");
                setNextDialogue();
            }
        }
    }
    public void interactButtonPulsed()
    {
        playerPulsedBoton = true;
    }
    public void setNextDialogue()
    {
        if(listOfDialogues.Length != currentDialogue)
        {
            currentDialogue++;
            dialogueLines = listOfDialogues[currentDialogue];
        }
    }
    public void SetSpeakerName()
    {
        if (dialogueLines.dialgos[lineIndex].hablador != "" && dialogueLines.dialgos[lineIndex].hablador != dialogueLines.hablador)
        {
            //Debug.Log(dialogueLines.dialgos[lineIndex].hablador);
            speakerNameText.text = dialogueLines.dialgos[lineIndex].hablador;
        }
        else
            speakerNameText.text = dialogueLines.hablador;
    }
}
