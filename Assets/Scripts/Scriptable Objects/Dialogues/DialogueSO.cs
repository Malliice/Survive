using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue", fileName = "dialogue_")]
public class DialogueSO : ScriptableObject
{
    public TextAsset inkJSONAsset = null;
    
    public List<SpeakerData> speakers;

    public int priority;
    
    public UnityEvent dialogueEvent;
}
