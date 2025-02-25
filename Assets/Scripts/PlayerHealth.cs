using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{	
	[SerializeField, Range(1, 10)] uint startingHealth = 5;
	[SerializeField] CinemachineVirtualCamera deathCamera;
	[SerializeField] Transform weaponCamera;
	[SerializeField] Image[] shieldBars;
	[SerializeField] GameObject gameOverContainer;

	uint currentHealth;
	StarterAssetsInputs starterAssetsInputs;

	const int deathCameraPriority = 20;



	void Awake ()
	{
		currentHealth = startingHealth;
	}


	void Start ()
	{
		// Cursor in game mode.
		starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
		starterAssetsInputs.SetCursorState(true);

		AdjustShieldUI();	// Allows for starting at <10 health.
	}


	public void TakeDamage (uint amount)
	{
		if (amount < currentHealth)
		{
			currentHealth -= amount;
			AdjustShieldUI();
		}
		else
		{
			LoseGame();
		}
	}


	void LoseGame ()
	{
		weaponCamera.parent = null;
		deathCamera.Priority = deathCameraPriority;

		currentHealth = 0;
		AdjustShieldUI();

		gameOverContainer.SetActive(true);
		starterAssetsInputs.SetCursorState(false);

		Destroy(this.gameObject);
	}


	private void AdjustShieldUI ()
	{
		for (int i = 0; i < shieldBars.Length; i++)
		{
			shieldBars[i].gameObject.SetActive( i < currentHealth );	// Boolean expression instead of if condition. Index reflects currentHealth shifted by -1 since array count starts at 0 (health 1), so no <= condition.
		}
	}
}
