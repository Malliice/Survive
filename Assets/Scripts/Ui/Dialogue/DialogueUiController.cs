using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUiController : MonoBehaviour
{
    [SerializeField] private GameManagerData gameData;
    
    [SerializeField] private GameObject dialogueScreen;
    [SerializeField] private TextMeshProUGUI txtDialogue;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private Image imgSpeaker;

    public bool dialogueActive;
    
    private DialogueSO currentDialogue;
    private Story currentStory;

    private const string SPEAKERTAG = "speaker";
    
    public void InitializeDialogue(DialogueSO dialogue)
    {
        //Initialize the variables
        currentDialogue = dialogue;
        currentStory = new Story(dialogue.inkJSONAsset.text);
        
        //Sets the story to the beginning
        currentStory.Continue();
        UpdateDialogue();
        
        //Activates the ui
        dialogueScreen.SetActive(true);
        dialogueActive = true;
        gameData.isWorldActive = !dialogueActive;
    }

    private void Update()
    {
        if(currentDialogue == null)
            return;
        if(currentStory == null)
            return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //If our dialogue counter is still under the dialogue's line total count we continue, or else we finish the dialogue
            if (currentStory.canContinue)
            {
                currentStory.Continue();
                UpdateDialogue();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    //Continue the story here
    void UpdateDialogue()
    {
        //Update the current line
        int currentSpeakerId = HandleSpeakerTag(currentStory.currentTags);
        SpeakerData currentSpeaker = currentDialogue.speakers[currentSpeakerId];
        string currentStoryLine = currentStory.currentText;
        
        //Update the UI
        txtName.text = currentSpeaker.speakerName;
        imgSpeaker.sprite = currentSpeaker.speakerSprite;
        txtDialogue.text = currentStoryLine;
    }

    //End story here
    void EndDialogue()
    {
        //We trigger the dialogue event at the end
        currentDialogue.dialogueEvent.Invoke();
        
        //We reset the ui
        txtName.text = "";
        imgSpeaker.sprite = null;
        txtDialogue.text = "";
        dialogueScreen.SetActive(false);
        
        //We empty out the variables
        dialogueActive = false;
        gameData.isWorldActive = !dialogueActive;
        currentStory = null;
    }

    private int HandleSpeakerTag(List<string> currentTags)
    {
        string currentTag = currentTags[0];
        string[] splitTag = currentTag.Split(':');
        if(splitTag.Length != 2)
            Debug.LogError("Tag could not be parsed: " + currentTag);
        string tagKey = splitTag[0];
        string tagValue = splitTag[1];
        switch (tagKey)
        {
            case SPEAKERTAG:
                return int.Parse(tagValue);
            default:
                Debug.LogWarning("Tag came in, but is not the right one: " + currentTag);
                return 0;
        }
    }
}
