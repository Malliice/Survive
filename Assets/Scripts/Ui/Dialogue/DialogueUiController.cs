using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUiController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueScreen;
    [SerializeField] private TextMeshProUGUI txtDialogue;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private Image imgSpeaker;

    public bool dialogueActive;
    
    private DialogueSO currentDialogue;

    private int currentDialogueCounter;

    public void InitializeDialogue(DialogueSO dialogue)
    {
        currentDialogue = dialogue;
        currentDialogueCounter = 0;
        UpdateDialogue();
        dialogueScreen.SetActive(true);
        dialogueActive = true;
    }

    void UpdateDialogue()
    {
        DialogueSO.DialogueLine currentLine = currentDialogue.dialogues[currentDialogueCounter];
        txtName.text = currentLine.speaker.speakerName;
        imgSpeaker.sprite = currentLine.speaker.speakerSprite;
        txtDialogue.text = currentLine.dialogue;
    }

    private void Update()
    {
        if(currentDialogue == null)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            currentDialogueCounter++;
            if(currentDialogueCounter >= currentDialogue.dialogues.Count)
                EndDialogue();
            else
                UpdateDialogue();
        }
    }

    void EndDialogue()
    {
        currentDialogue.dialogueEvent.Invoke();
        dialogueScreen.SetActive(false);
        dialogueActive = false;
    }
}
