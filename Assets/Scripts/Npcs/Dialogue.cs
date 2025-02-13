using UnityEngine;
using System.Collections;
using TMPro;
using Assets.SimpleLocalization.Scripts;

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
        LocalizationManager.OnLocalizationChanged += UpdateLocalizedText;
    }

    void OnDestroy()
    {
        LocalizationManager.OnLocalizationChanged -= UpdateLocalizedText;
    }

    // Update is called once per frame
    void Update()
    {
        if (listOfDialogues.Length != currentDialogue + 1)
        {
            UpdateDialogues();
        }
        if (isPlayerInRange && playerPulsedBoton)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == LocalizationManager.Localize(dialogueLines.dialgos[lineIndex].texto))
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = LocalizationManager.Localize(dialogueLines.dialgos[lineIndex].texto);

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

        string localizedText = LocalizationManager.Localize(dialogueLines.dialgos[lineIndex].texto);

        foreach (char ch in localizedText)
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
            StopAllCoroutines();
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
        string speakerKey = (dialogueLines.dialgos[lineIndex].hablador != "" && dialogueLines.dialgos[lineIndex].hablador != dialogueLines.hablador)
            ? dialogueLines.dialgos[lineIndex].hablador
            : dialogueLines.hablador;

        speakerNameText.text = LocalizationManager.Localize(speakerKey);
    }
    void UpdateDialogues()
    {
        if(listOfDialogues[currentDialogue + 1].misionCode != "")
        {
            if (FindAnyObjectByType<MisionManager>().EstaAceptadaLaMision(listOfDialogues[currentDialogue + 1].misionCode))
            {
                if (FindAnyObjectByType<SeguimientoMisionPrincipal>().GetMisionActual() == listOfDialogues[currentDialogue + 1].misionCode)
                {
                    setNextDialogue();
                }
            }
        }
    }

    void UpdateLocalizedText()
    {
        if (didDialogueStart)
        {
            dialogueText.text = LocalizationManager.Localize(dialogueLines.dialgos[lineIndex].texto);
            speakerNameText.text = LocalizationManager.Localize(dialogueLines.hablador);
        }
    }
}
