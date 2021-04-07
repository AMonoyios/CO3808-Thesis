using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(EnemyUIHandler))]
public class PlayerAttack : InteractPoint
{
    AllCharacterStats characterStats;
    private FocusController focusController;

    [Header("Specific Enemy Variables")]
    public EnemyBlueprint enemy;
    
    public float health;

    public void Start()
    {
        playerManager = PlayerManager.Instance.player.GetComponent<PlayerManager>();
        characterStats = playerManager.player.GetComponent<AllCharacterStats>();

        focusController = playerManager.player.GetComponent<FocusController>();

        if (interactionPoint == null)
        {
            interactionPoint = this.transform;
        }

        // set the health on spawn of enemy
        health = enemy.maxHealth;
    }

    public override void Interact()
	{
		base.Interact();

        // player attack the enemy
        AttackEnemy();
    }

    void AttackEnemy()
    {
		if (health <= 0)
		{
            // drop loot
			for (int i = 0; i < enemy.loot.Count; i++)
			{
                Debug.Log("DEBUG - Ai: Dropping loot " + enemy.loot[i].ItemName);

                GameObject loot = Instantiate(enemy.loot[i].itemPrefab, focusController.focus.interactionPoint.position, focusController.focus.interactionPoint.rotation);
                loot.name = enemy.loot[i].ItemName;
			}

            // kill enemy
            Destroy(gameObject);
		}
        else
        {
            float playerEnemyDistance = Vector3.Distance(playerManager.player.transform.position, this.transform.position);
            if (playerEnemyDistance <= enemy.enemyAttackRadius)
		    {
                // attacking the enemy
                Debug.Log("DEBUG - Ai: Player dealed " + characterStats.Damage + " to " + enemy.prefabName);
                health -= characterStats.Damage;
            }
		}

        // defocusing enemy
        focusController.DeFocus();
    }
}
