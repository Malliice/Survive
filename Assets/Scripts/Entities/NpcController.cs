using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NpcController : MonoBehaviour
{
    private DialogueUiController dialogueController;
    
    [Header("Stats")]
    [SerializeField] private int healthMax;
    [SerializeField] private int affection;
    private bool hasDialogue = false;
    
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject npcTxt;

    [SerializeField] private NpcData npcData;
    // [SerializeField] private int npcId;

    [SerializeField] private UnityEvent deathEvent;

    private void Start()
    {
        dialogueController = FindObjectOfType<DialogueUiController>();
        hasDialogue = false;
        StartSceneInitialization();
    }

    void StartSceneInitialization()
    {
        //A chaque entrée dans la scène, les pnj perdent en vie
        npcData.health--;

        if (npcData.health <= 0)
        {
            deathEvent.Invoke();
            gameObject.SetActive(false);
        }
        
        UpdateUi();
    }

    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 2);
        npcTxt.SetActive(false);
        foreach (var col in cols)
        {
            if (col.gameObject.CompareTag("Player")) 
                npcTxt.SetActive(true);
        }
    }

    #region Interactions

    public void Feed()
    {
        if(npcData.health >= healthMax)
            return;
        npcData.health++;
        
        UpdateUi();
    }

    void UpdateUi()
    {
        healthBar.fillAmount = (float)npcData.health / healthMax;
    }

    public void StartDialogue()
    {
        if(npcData.activeDialoguePool.Count <= 0)
            return;
        
        if(dialogueController.dialogueActive)
            return;
        
        if(hasDialogue)
            return;
        
        //Create a temp pool where we stack all the top priority dialogues
        DialogueSO topPriorityDialogue = null;
        int maxPriority = int.MinValue;
        foreach (var dialogue in npcData.activeDialoguePool)
        {
            if (dialogue.priority > maxPriority)
            {
                topPriorityDialogue = dialogue;
                maxPriority = dialogue.priority;
            }
        }

        hasDialogue = true;
        dialogueController.InitializeDialogue(topPriorityDialogue);
        
        //On supprime le dialogue de la pool active
        npcData.usedDialoguePool.Add(topPriorityDialogue);
        npcData.activeDialoguePool.Remove(topPriorityDialogue);
    }

    public void AddAffection()
    {
        if (npcData.affectionDialoguePool.Count <= 0)
            return;
        
        affection++;
        DialogueSO dialogueToAdd = npcData.affectionDialoguePool[0];
        npcData.affectionDialoguePool.RemoveAt(0);
        dialogueController.InitializeDialogue(dialogueToAdd);
    }
    
    #endregion
}
