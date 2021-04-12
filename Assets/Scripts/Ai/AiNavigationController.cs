using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCState
{
    idle,
    focused
}

public enum EnemyState
{
    patrolling,
    attacking,
    evading
}

public class AiNavigationController : MonoBehaviour
{
    private GameObject player;
    private AiSpawner spawner;
    private int indexInList;
    private EnemyState enemyState;
    private AllCharacterStats characterStats;

    [Header("Enemy Variables")]
    public Vector3 patrolDestination;
    public bool reachedDesination = false;
    public bool isAttacking = false;
    public float attackTimer = 5.0f;
    private float timer;
    private float totalAttackChance;

    [Header("Navigation Variables")]
    public string navigationName;

    [Header("Gizmo Reference")]
    // Cache Gizmos Script
    public GizmosManager gizmos;

    void Awake()
    {
        gizmos = GameObject.Find("GizmoManager").GetComponent<GizmosManager>();
    }

    private void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player");
        characterStats = player.GetComponent<AllCharacterStats>();

		if (navigationName == "")
		{
            Debug.LogError("ERROR - Ai: Navigation was not assigned!");
		}
		else 
		{
            spawner = GameObject.Find(navigationName).GetComponent<AiSpawner>();

            if (spawner.characterType == CharacterType.Enemy)
            {
                for (int i = 0; i < spawner.EnemyBlueprints.Count; i++)
			    {
				    if (this.name == spawner.EnemyBlueprints[i].prefabName)
				    {
                        indexInList = i;
                    
                        totalAttackChance = spawner.EnemyBlueprints[i].smallAttackChance + 
                                            spawner.EnemyBlueprints[i].bigAttackChance + 
                                            spawner.EnemyBlueprints[i].critiaclChance;
                    }
			    }
		    }
		    else
		    {
                for (int i = 0; i < spawner.passiveNPCBlueprints.Count; i++)
                {
                    if (this.name == spawner.passiveNPCBlueprints[i].prefabName)
                    {
                        indexInList = i;
                    }
                }
            }

            timer = attackTimer;
		}
    }

	// Update is called once per frame
	void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
		if (spawner.characterType == CharacterType.Enemy)
		{
            enemyState = spawner.EnemyBlueprints[indexInList].enemyState;
            
			if (enemyState == EnemyState.patrolling && distanceToPlayer <= spawner.EnemyBlueprints[indexInList].enemySpotRadius)
			{
                enemyState = EnemyState.attacking;
            }
            else if (enemyState == EnemyState.attacking && distanceToPlayer >= spawner.EnemyBlueprints[indexInList].enemyForgetRadius)
            {
                enemyState = EnemyState.patrolling;
            }

			switch (enemyState)
			{
				case EnemyState.patrolling:
					{
                        Patrolling();
					}
                    break;
				case EnemyState.attacking:
					{
                        ChasingPlayer();

						if (isAttacking && attackTimer > 0)
						{
                            attackTimer -= Time.deltaTime;
						}
						else if (attackTimer <= 0)
						{
                            AttackAttempt();

                            attackTimer = timer;
                        }
                    }
                    break;
				case EnemyState.evading:
					break;
				default:
					break;
			}
        }
		else
		{
            NPCState npcState = spawner.passiveNPCBlueprints[indexInList].npcState;
        }
    }

    void Patrolling()
	{
        Vector3 destinationDirection = patrolDestination - transform.position;
        destinationDirection.y = 0;
        
        float destinationDistance = destinationDirection.magnitude;
        
        if (destinationDistance >= spawner.EnemyBlueprints[indexInList].enemyAttackRadius)
        {
            reachedDesination = false;
        
            Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);

			if (spawner.characterType == CharacterType.Enemy)
			{
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, spawner.EnemyBlueprints[indexInList].rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * spawner.EnemyBlueprints[indexInList].movingSpeed * Time.deltaTime);
			}
			else
			{
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, spawner.passiveNPCBlueprints[indexInList].rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * spawner.passiveNPCBlueprints[indexInList].movingSpeed * Time.deltaTime);
            }
        }
        else
        {
            reachedDesination = true;
        }

        attackTimer = timer;
    }

    void ChasingPlayer()
	{
        Vector3 playerPosition = player.transform.position;
        
        Vector3 destinationDirection = playerPosition - this.transform.position;
        destinationDirection.y = 0;

        float destinationDistance = Vector3.Distance(this.transform.position, playerPosition);
        
        if (destinationDistance >= spawner.EnemyBlueprints[indexInList].enemyAttackRadius)
        {
            transform.Translate(Vector3.forward * spawner.EnemyBlueprints[indexInList].movingSpeed * Time.deltaTime);

            isAttacking = false;
        }
        else
        {
            isAttacking = true;
        }

        Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, spawner.EnemyBlueprints[indexInList].rotationSpeed * Time.deltaTime);
    }

    void AttackAttempt()
	{
        EnemyBlueprint enemy = spawner.EnemyBlueprints[indexInList];
        
        Debug.Log(enemy.prefabName + " is attacking the player");

        float randAttack = Random.value * 100;
		if (randAttack < totalAttackChance)
		{
            Attack(randAttack, enemy);
		}
		else
		{
            Debug.Log(enemy.prefabName + " missed the attack");
		}
	}

    void Attack(float randAttack, EnemyBlueprint enemy)
	{
		if (randAttack <= enemy.critiaclChance)
		{
            Debug.Log(enemy.prefabName + " performs a critical attack: " + randAttack);
            characterStats.Health -= enemy.criticalAttack - ((enemy.criticalAttack * characterStats.Protection) / 100);
		}
		else if (randAttack > enemy.critiaclChance && randAttack <= enemy.bigAttackChance)
		{
            Debug.Log(enemy.prefabName + " performs a big attack: " + randAttack);
            characterStats.Health -= enemy.bigAttack - ((enemy.bigAttack * characterStats.Protection) / 100);
        }
		else
		{
            Debug.Log(enemy.prefabName + " performs a small attack: " + randAttack);
            characterStats.Health -= enemy.smallAttack - ((enemy.smallAttack * characterStats.Protection) / 100);
        }

        isAttacking = false;
	}

    public void SetDestination(Vector3 destination)
    {
        this.patrolDestination = destination;
        reachedDesination = false;
    }

    // visualize the npc state
    void OnDrawGizmos()
    {
        if (spawner.characterType == CharacterType.Enemy)
        {
            EnemyBlueprint enemy = spawner.EnemyBlueprints[indexInList];

            switch (enemyState)
			{
				case EnemyState.patrolling:
                    {
                        Gizmos.color = gizmos.PatrolingEnemyGizmo * 1.0f;
                        Gizmos.DrawWireSphere(transform.position, enemy.enemySpotRadius);

                        Gizmos.color = gizmos.AttackingEnemyGizmo * 0.5f;
                        Gizmos.DrawWireSphere(transform.position, enemy.enemyAttackRadius);

                        Gizmos.color = gizmos.EvadingEnemyGizmo * 0.5f;
                        Gizmos.DrawWireSphere(transform.position, enemy.enemyForgetRadius);
                    }
                    break;
				case EnemyState.attacking:
					{
                        Gizmos.color = gizmos.PatrolingEnemyGizmo * 0.5f;
                        Gizmos.DrawWireSphere(transform.position, enemy.enemySpotRadius);

                        Gizmos.color = gizmos.AttackingEnemyGizmo * 1.0f;
                        Gizmos.DrawWireSphere(transform.position, enemy.enemyAttackRadius);

                        Gizmos.color = gizmos.EvadingEnemyGizmo * 0.5f;
                        Gizmos.DrawWireSphere(transform.position, enemy.enemyForgetRadius);
                    }
                    break;
				case EnemyState.evading:
					{
                        Gizmos.color = gizmos.PatrolingEnemyGizmo * 0.5f;
                        Gizmos.DrawWireSphere(transform.position, enemy.enemySpotRadius);

                        Gizmos.color = gizmos.AttackingEnemyGizmo * 0.5f;
                        Gizmos.DrawWireSphere(transform.position, enemy.enemyAttackRadius);

                        Gizmos.color = gizmos.EvadingEnemyGizmo * 1.0f;
                        Gizmos.DrawWireSphere(transform.position, enemy.enemyForgetRadius);
                    }
                    break;
				default:
					break;
			}
        }
        else
        {
			switch (spawner.passiveNPCBlueprints[indexInList].npcState)
			{
				case NPCState.idle:
					{
                        Gizmos.color = gizmos.IdleNPCGizmo * 1.0f;
                        Gizmos.DrawWireSphere(transform.position, spawner.passiveNPCBlueprints[indexInList].npcInteractionRadius);

                        Gizmos.color = gizmos.FocusedNPCGizmo * 0.5f;
                        Gizmos.DrawWireSphere(transform.position, spawner.passiveNPCBlueprints[indexInList].npcInteractionRadius);

                    }
                    break;
				case NPCState.focused:
                    {
                        Gizmos.color = gizmos.IdleNPCGizmo * 0.5f;
                        Gizmos.DrawWireSphere(transform.position, spawner.passiveNPCBlueprints[indexInList].npcInteractionRadius);

                        Gizmos.color = gizmos.FocusedNPCGizmo * 1.0f;
                        Gizmos.DrawWireSphere(transform.position, spawner.passiveNPCBlueprints[indexInList].npcInteractionRadius);

                    }
                    break;
				default:
					break;
			}

        }
    }
}
