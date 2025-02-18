using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] ParticleSystem muzzleFlash;
	[SerializeField] uint damageAmount = 1;

	StarterAssetsInputs starterAssetsInputs;


	void Awake()
	{
		starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
	}


    void Update()
	{
		HandleShoot();
	}


	private void HandleShoot ()
	{
		if (!starterAssetsInputs.shoot) return;
		
		muzzleFlash.Play();

		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity))
		{
			EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
			enemyHealth?.TakeDamage(damageAmount);
		}

		starterAssetsInputs.ShootInput(false);
	}
}
