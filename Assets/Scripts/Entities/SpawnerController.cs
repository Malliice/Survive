using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private List<EnemyController> enemySpawned = new List<EnemyController>();

    private float spawningTime;
    [SerializeField] private float spawningTimeMax;
    public bool spawningActive;

    private void Awake()
    {
        spawningActive = true;
    }

    private void Update()
    {
        if (!spawningActive)
            return;

        spawningTime += Time.deltaTime;
        
        if (spawningTime >= spawningTimeMax)
        {
            spawningTime = 0;
            enemySpawned.Add(Instantiate(enemyPrefab, transform.position, Quaternion.identity)
                .GetComponent<EnemyController>());
            if (enemySpawned.Count >= 3)
                spawningActive = false;
        }
    }
}
