using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "event_", menuName = "Scriptable Objects/Event")]
public class EventSO : ScriptableObject
{
    public UnityEvent eventToInvoke;
}
