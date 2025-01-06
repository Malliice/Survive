using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManagerData gameData;
    
    [Header("Stats")]
    [SerializeField] private int health;
    [SerializeField] private int dmg;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float stunTime;

    //References
    private Vector3 destination;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    public enum EnemyState
    {
        NEUTRAL,
        AGRESSIVE,
        STUN
    }

    public EnemyState enemyState;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Block everything if game is not active
        if (!gameData.isWorldActive)
        {
            navMeshAgent.isStopped = true;
            rb.velocity = Vector3.zero;
            return;
        }

        navMeshAgent.isStopped = false;
        
        Collider[] cols = Physics.OverlapSphere(transform.position, detectionRadius);
        enemyState = EnemyState.NEUTRAL;
        foreach (var c in cols)
        {
            if (c.gameObject.CompareTag("Player"))
            {
                destination = c.transform.position;
                enemyState = EnemyState.AGRESSIVE;
            }
        }

        if (enemyState == EnemyState.AGRESSIVE)
        {
            navMeshAgent.destination = destination;
        }
    }

    public void GetHit(int dmg, Vector3 dir)
    {
        health -= Mathf.Abs(dmg);
        if (health <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        rb.AddForce(dir, ForceMode.VelocityChange);
        enemyState = EnemyState.STUN;
        StartCoroutine(Delay(stunTime, () => 
        { 
            enemyState = EnemyState.NEUTRAL; 
            rb.velocity = Vector3.zero;
        }));
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().LoseHealth(dmg);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    IEnumerator Delay(float delayTime, Action delayedAction)
    {
        yield return new WaitForSeconds(delayTime);
        delayedAction.Invoke();
    }
}
