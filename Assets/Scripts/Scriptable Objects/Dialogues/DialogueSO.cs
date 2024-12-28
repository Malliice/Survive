using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue", fileName = "dialogue_")]
public class DialogueSO : ScriptableObject
{
    [Serializable]
    public struct DialogueLine
    {
        [TextArea] public string dialogue;
        public SpeakerData speaker;
    }

    public int priority;
    public List<DialogueLine> dialogues;
    public UnityEvent dialogueEvent;
}
