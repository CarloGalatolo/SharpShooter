using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] GameObject explosionVFXPrefab;
	[SerializeField] uint startingHealth = 3;

	uint currentHealth;
	GameManager gameManager;



	void Awake ()
	{
		currentHealth = startingHealth;
	}


	void Start ()
	{
		gameManager = FindFirstObjectByType<GameManager>();
		gameManager.AdjustEnemiesLeft(1);
	}


	public void TakeDamage (uint amount)
	{
		if (amount < currentHealth)
		{
			currentHealth -= amount;
		}
		else
		{
			SelfDestruct();
		}
	}


	public void SelfDestruct ()
	{
		Instantiate(explosionVFXPrefab, transform.position, Quaternion.identity);
		gameManager.AdjustEnemiesLeft(-1);
		Destroy(this.gameObject);
	}
}
