using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCState
{
    none,
    idle,
    focused
}

public enum EnemyState
{
    none,
    patrolling,
    attacking,
    evading
}

public class AiNavigationController : MonoBehaviour
{
    private GameObject player;
    private PedestrianSpawner spawner;

    [Header("Gizmo Reference")]
    // Cache Gizmos Script
    public GizmosManager gizmos;

    [Header("Navigation Properties")]
    public float movementSpeed = 1.0f;
    public float rotationSpeed = 120.0f;
    public float stopDistance = 2.5f;
    public Vector3 patrolDestination;
    public bool reachedDesination = false;

    [Header("Enemy Properties")]
    public int randomEnemyBP;

    void Awake()
    {
        gizmos = GameObject.Find("GizmoManager").GetComponent<GizmosManager>();
    }

    private void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player");

        spawner = GameObject.Find("AiWaypoints").GetComponent<PedestrianSpawner>();
        randomEnemyBP = Random.Range(0, spawner.enemyBlueprints.Count);

		if (spawner.enemyBlueprints[randomEnemyBP].isEnemy)
		{
            spawner.enemyBlueprints[randomEnemyBP].enemyState = EnemyState.patrolling;
		}
		else
		{
            spawner.enemyBlueprints[randomEnemyBP].npcState = NPCState.idle;
		}
	}

	// Update is called once per frame
	void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
		if (spawner.enemyBlueprints[randomEnemyBP].enemyState == EnemyState.patrolling && distanceToPlayer <= spawner.enemyBlueprints[randomEnemyBP].enemyInteractionRadius)
		{
            spawner.enemyBlueprints[randomEnemyBP].enemyState = EnemyState.attacking;
		}
		else if (spawner.enemyBlueprints[randomEnemyBP].enemyState == EnemyState.attacking && distanceToPlayer >= spawner.enemyBlueprints[randomEnemyBP].enemyForgetRadius)
   		{
            spawner.enemyBlueprints[randomEnemyBP].enemyState = EnemyState.patrolling;
        }

		if (spawner.enemyBlueprints[randomEnemyBP].npcState == NPCState.idle || spawner.enemyBlueprints[randomEnemyBP].enemyState == EnemyState.patrolling)
		{
            if (transform.position != patrolDestination)
            {
                Patrolling();
            }
		}
		else if (spawner.enemyBlueprints[randomEnemyBP].enemyState == EnemyState.attacking)
		{
            Attacking();
        }
    }

    void Patrolling()
	{
        Vector3 destinationDirection = patrolDestination - transform.position;
        destinationDirection.y = 0;

        float destinationDistance = destinationDirection.magnitude;

        if (destinationDistance >= stopDistance)
        {
            reachedDesination = false;

            Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            reachedDesination = true;
        }
    }

    void Attacking()
	{
        Vector3 playerPosition = player.transform.position;

        if (transform.position != playerPosition)
        {
            Vector3 destinationDirection = playerPosition - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {

            }
        }
    }

    public void SetDestination(Vector3 destination)
    {
        this.patrolDestination = destination;
        reachedDesination = false;
    }

    // visualize the npc state
    void OnDrawGizmos()
    {
		switch (spawner.enemyBlueprints[randomEnemyBP].npcState)
		{
			case NPCState.idle:
				{
                    Gizmos.color = gizmos.IdleNPCGizmo * gizmos.IdleNPCIntensity;
                    Gizmos.DrawWireSphere(transform.position, spawner.enemyBlueprints[randomEnemyBP].npcInteractionRadius);
				}
				break;
			case NPCState.focused:
				{
                    Gizmos.color = gizmos.FocusedNPCGizmo * gizmos.FocusedNPCIntensity;
                    Gizmos.DrawWireSphere(transform.position, spawner.enemyBlueprints[randomEnemyBP].npcInteractionRadius);
                }
                break;
			default:
				break;
		}

		switch (spawner.enemyBlueprints[randomEnemyBP].enemyState)
		{
			case EnemyState.patrolling:
				{
                    Gizmos.color = gizmos.PatrolingEnemyGizmo * gizmos.PatrolingEnemyIntensity;
                    Gizmos.DrawWireSphere(transform.position, spawner.enemyBlueprints[randomEnemyBP].enemyInteractionRadius);
                }
				break;
			case EnemyState.attacking:
				{
                    Gizmos.color = gizmos.AttackingEnemyGizmo * gizmos.AttackingEnemyIntensity;
                    Gizmos.DrawWireSphere(transform.position, spawner.enemyBlueprints[randomEnemyBP].enemyInteractionRadius);
                }
				break;
			case EnemyState.evading:
				{
                    Gizmos.color = gizmos.EvadingEnemyGizmo * gizmos.EvadingEnemyIntensity;
                    Gizmos.DrawWireSphere(transform.position, spawner.enemyBlueprints[randomEnemyBP].enemyInteractionRadius);
                }
                break;
			default:
				break;
		}
	}
}
