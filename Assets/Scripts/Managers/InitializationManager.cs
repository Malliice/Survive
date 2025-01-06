using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationManager : MonoBehaviour
{
    public PlayerData playerData;
    public GameManagerData gameData;
    public List<NpcData> activeNpcData;
    public List<NpcData> defaultNpcData;
    [SerializeField] private SurvivalWorldData survivalWorldData;

    private void Awake()
    {
        InitializationManager[] managers = FindObjectsOfType<InitializationManager>();

        if (managers.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        gameData.isWorldActive = true;
        
        for (int i = 0; i < defaultNpcData.Count; i++)
        {
            activeNpcData[i].health = defaultNpcData[i].health;
            activeNpcData[i].baseDialoguePool = new List<DialogueSO>(defaultNpcData[i].baseDialoguePool);
            activeNpcData[i].affectionDialoguePool = new List<DialogueSO>(defaultNpcData[i].affectionDialoguePool);
            
            activeNpcData[i].InitDialoguePools();
        }

        playerData.health = playerData.healthMax;

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "SurvivalScene")
            {
                survivalWorldData.SpawnObjects();
            }
        };
    }
}
