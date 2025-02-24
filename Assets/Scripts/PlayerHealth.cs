using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{	
	[SerializeField, Range(1, 10)] uint startingHealth = 5;
	[SerializeField] CinemachineVirtualCamera deathCamera;
	[SerializeField] Transform weaponCamera;
	[SerializeField] Image[] shieldBars;

	uint currentHealth;

	const int deathCameraPriority = 20;



	void Awake ()
	{
		currentHealth = startingHealth;
	}


	void Start ()
	{
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
			weaponCamera.parent = null;
			deathCamera.Priority = deathCameraPriority;

			currentHealth = 0;
			AdjustShieldUI();
			Destroy(this.gameObject);
		}
	}


	private void AdjustShieldUI ()
	{
		for (int i = 0; i < shieldBars.Length; i++)
		{
			shieldBars[i].gameObject.SetActive( i < currentHealth );	// Boolean expression instead of if condition. Index reflects currentHealth shifted by -1 since array count starts at 0 (health 1), so no <= condition.
		}
	}
}
