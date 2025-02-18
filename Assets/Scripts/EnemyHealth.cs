using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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
			Destroy(this.gameObject);
		}
	}
}
