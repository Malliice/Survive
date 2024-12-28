using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Survival World", fileName = "SurvivalWorldData")]
public class SurvivalWorldData : ScriptableObject
{
    public struct ObjectData
    {
        public Vector3 position;
        public GameObject objectPrefab;

        public ObjectData(Vector3 pos, GameObject prefab)
        {
            position = pos;
            objectPrefab = prefab;
        }
    }
    List<ObjectData> objectDatas = new List<ObjectData>();

    public Vector3 playerLastPos;

    public void AddObject(ObjectData objectData)
    {
        objectDatas.Add(objectData);
    }

    public void SpawnObjects()
    {
        foreach (var objectData in objectDatas)
        {
            Instantiate(objectData.objectPrefab, objectData.position, Quaternion.identity);
        }
    }
}
