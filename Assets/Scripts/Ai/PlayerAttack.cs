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
		if (health <= playerManager.player.GetComponent<AllCharacterStats>().Damage)
		{
            // drop loot
			for (int i = 0; i < enemy.loot.Count; i++)
			{
				if (enemy.loot[i].item != null)
				{
                    float chance = Random.Range(0.1f, 99.9f);
                    if (chance < enemy.loot[i].dropChance)
				    {
                        Debug.Log("DEBUG - Ai: Dropping loot " + enemy.loot[i].item.ItemName);

                        GameObject loot = Instantiate(enemy.loot[i].item.itemPrefab, focusController.focus.interactionPoint.position, focusController.focus.interactionPoint.rotation);
                        loot.name = enemy.loot[i].item.ItemName;
				    }
				    else
				    {
                        Debug.Log("DEBUG - Ai: Lost chance of dropping " + enemy.loot[i].item.ItemName + " - " + chance.ToString());
				    }
				}
				else
				{
                    Debug.LogWarning("WARNING - Ai: Drop item with index " + i + " is not assigned!");
				}
			}

            // drop currency
            for (int i = 0; i < enemy.currency.Count; i++)
            {
                if (enemy.currency[i].currency != null)
                {
                    float chance = Random.Range(0.1f, 99.9f);
                    if (chance < enemy.currency[i].dropChance)
                    {
                        Debug.Log("DEBUG - Ai: Dropping currency " + enemy.currency[i].currency.CurrencyName);

                        GameObject currency = Instantiate(enemy.currency[i].currency.currencyPrefab, focusController.focus.interactionPoint.position, focusController.focus.interactionPoint.rotation);
                        currency.name = enemy.currency[i].currency.CurrencyName;
                    }
                    else
                    {
                        Debug.Log("DEBUG - Ai: Lost chance of dropping " + enemy.currency[i].currency.CurrencyName + " - " + chance.ToString());
                    }
                }
                else
                {
                    Debug.LogWarning("WARNING - Ai: Drop currency with index " + i + " is not assigned!");
                }
            }

            focusController.DeFocus();

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

            // defocusing enemy
            focusController.DeFocus();
        }
        
    }
}
