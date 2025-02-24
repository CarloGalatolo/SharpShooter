using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] GameObject explosionVFXPrefab;
	[SerializeField] uint startingHealth = 3;

	uint currentHealth;



	void Awake ()
	{
		currentHealth = startingHealth;
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
		Destroy(this.gameObject);
	}
}
