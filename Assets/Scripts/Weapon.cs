using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] ParticleSystem muzzleFlash;
	[SerializeField] GameObject hitVFXPrefab;
	[SerializeField] uint damageAmount = 1;

	StarterAssetsInputs starterAssetsInputs;
	Animator animator;

	const string SHOOT_ANIMATION = "Shoot";


	void Awake()
	{
		starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
		animator = GetComponentInParent<Animator>();
	}


    void Update()
	{
		HandleShoot();
	}


	private void HandleShoot ()
	{
		if (!starterAssetsInputs.shoot) return;
		starterAssetsInputs.ShootInput(false);	// Reset input value.
		
		muzzleFlash.Play();
		animator.Play(SHOOT_ANIMATION, 0, 0);

		if ( Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity) )
		{
			Instantiate(hitVFXPrefab, hit.point, Quaternion.identity);	// Shot hit VFX;
			EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();	// Check if hit an enemy.
			enemyHealth?.TakeDamage(damageAmount);	// Damage enemy if hit.
		}
	}
}
