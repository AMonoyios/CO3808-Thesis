using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : InteractPoint
{
    public Scene TargetScene;
    public int ToggleSceneIndex;

    void Start()
    {
        playerManager = PlayerManager.Instance.player.GetComponent<PlayerManager>();
    }

    public override void Interact()
    {
        base.Interact();

        TeleportToTarget();
    }

    void TeleportToTarget()
	{
        int TotalScenes = SceneManager.sceneCount;

		if (playerManager.player.scene.buildIndex >= TotalScenes)
		{
            ToggleSceneIndex = 0;
		}
		else
		{
            ToggleSceneIndex = playerManager.player.scene.buildIndex + 1;
        }

        Debug.Log("Player Teleporting to " + ToggleSceneIndex);
	}
}
