using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Npc", fileName = "npc_")]
public class NpcData : ScriptableObject
{
    public int health;

    public List<DialogueSO> affectionDialoguePool = new List<DialogueSO>();
    public List<DialogueSO> baseDialoguePool = new List<DialogueSO>();
    public List<DialogueSO> activeDialoguePool = new List<DialogueSO>();
    public List<DialogueSO> usedDialoguePool = new List<DialogueSO>();

    public void InitDialoguePools()
    {
        activeDialoguePool.Clear();
        activeDialoguePool.AddRange(baseDialoguePool);
        usedDialoguePool.Clear();
    }

    public void AddToDialoguePool(DialogueSO newDialogue)
    {
        //Si le dialogue existe déjà dans les dialogues usés, on ne le rajoute pas, on l'ignore
        if(usedDialoguePool.Exists(x=>x == newDialogue))
            return;
        
        activeDialoguePool.Add(newDialogue);
    }
}
