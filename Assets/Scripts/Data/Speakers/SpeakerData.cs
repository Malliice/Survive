using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "speaker_", menuName = "Data/Speakers")]
public class SpeakerData : ScriptableObject
{
    public string speakerName;
    public Sprite speakerSprite;
}
