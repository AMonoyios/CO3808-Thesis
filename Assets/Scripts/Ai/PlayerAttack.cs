using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAttack : InteractPoint
{
    AllCharacterStats characterStats;
    private FocusController focusController;

    [Header("Specific Enemy Variables")]
    public TextMeshProUGUI nameUI;
    public Slider healthUI;
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
        healthUI.value = health;

        // display the enemy name above health UI
        nameUI.text = enemy.prefabName;
    }

    public override void Interact()
	{
		base.Interact();

        // player attack the enemy
        AttackEnemy();
    }

    void AttackEnemy()
    {
        float oldHealth = health;

		if (health <= 0)
		{
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

        // update the enemy health UI
        healthUI.value = Mathf.Lerp(oldHealth, health, 2.0f);

        // defocusing enemy
        focusController.DeFocus();
    }
}
