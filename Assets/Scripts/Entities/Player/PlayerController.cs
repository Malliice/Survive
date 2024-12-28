using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerData playerData;
    private Rigidbody rb;
    private PlayerInventoryController inventory;
    private Animator animator;
    [SerializeField] private EventSO playerDeathEvent;
    [SerializeField] private Transform weaponDirection;
    
    [Header("Stats")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float interactionRadius = 2;
    [SerializeField] private float atkRadius = 1;
    public int damage = 1;
    [HideInInspector] public ResourceContainerController.ToolNeeded currentTool;
    [SerializeField] private int knockbackStrength = 5;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inventory = GetComponent<PlayerInventoryController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = dir * movementSpeed;

        Vector3 weaponDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if(weaponDir != Vector3.zero)
            weaponDirection.position = transform.position + weaponDir;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
            Attack();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Talk();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GiveItem();
        }
    }

    #region Actions
    
    void Interact()
    {
        Collider[] cols = Physics.OverlapSphere(weaponDirection.position, interactionRadius);
        List<ResourceContainerController> resources = new List<ResourceContainerController>();
        List<NpcController> npc = new List<NpcController>();
        List<LootController> loots = new List<LootController>();
        foreach (var c in cols)
        {
            if (c.TryGetComponent(out ResourceContainerController resource))
                resources.Add(resource);
            
            if( c.TryGetComponent(out LootController loot))
                loots.Add(loot);

            if (c.TryGetComponent(out NpcController n))
                npc.Add(n);
        }

        if (resources.Count > 0)
        {
            // inventory.AddItem(resources[0].itemId);
            resources[0].DamageResourceContainer(currentTool);
            // resources[0].gameObject.SetActive(false);
            return;
        }

        if (loots.Count > 0)
        {
            for (int i = 0; i < loots[0].itemNbr; i++)
            {
                inventory.AddItem(loots[0].itemId);
            }
            
            loots[0].gameObject.SetActive(false);
            return;
        }

        if (npc.Count > 0)
        {
            //Si on a pas de nourriture, on ne nourrit personne
            if(!inventory.HasItem(0))
                return;

            inventory.RemoveItem(0);
            npc[0].Feed();
            return;
        }
    }

    void Attack()
    {
        animator.SetTrigger("atk");
        
        Collider[] cols = Physics.OverlapSphere(weaponDirection.position, interactionRadius);
        List<EnemyController> enemy = new List<EnemyController>();
        foreach (var c in cols)
        {
            if (c.TryGetComponent(out EnemyController e))
                enemy.Add(e);
        }
        if (enemy.Count > 0)
        {
            foreach (var e in enemy)
            {
                Vector3 direction = e.transform.position - transform.position;
                direction.Normalize();
                direction += Vector3.up;
                e.GetHit(damage, direction * knockbackStrength);
            }
        }
    }

    void Talk()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, interactionRadius);
        List<NpcController> npc = new List<NpcController>();
        foreach (var c in cols)
        {
            if (c.TryGetComponent(out NpcController n))
                npc.Add(n);
        }
        
        if (npc.Count > 0)
        {
            npc[0].StartDialogue();
        }
    }

    void GiveItem()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, interactionRadius);
        List<NpcController> npc = new List<NpcController>();
        foreach (var c in cols)
        {
            if (c.TryGetComponent(out NpcController n))
                npc.Add(n);
        }
        
        if (npc.Count > 0)
        {
            //Si on a pas de cadeau, on n'aide personne
            if(!inventory.HasItemByName("Gift"))
                return;

            inventory.RemoveItemByName("Gift");
            
            npc[0].AddAffection();
        }
    }
    
    #endregion
    
    public void LoseHealth(int dmg)
    {
        playerData.health -= Mathf.Abs(dmg);
        
        if(playerData.health <= 0)
            Death();
    }

    public void Death()
    {
        playerDeathEvent.eventToInvoke.Invoke();
    }
    
    public void ResetHealth()
    {
        playerData.health = playerData.healthMax;
    }

    private void OnDrawGizmosSelected()
    {
        // Gizmos.DrawWireSphere(transform.position, interactionRadius);
        Gizmos.DrawWireSphere(weaponDirection.position, interactionRadius);
    }
}
