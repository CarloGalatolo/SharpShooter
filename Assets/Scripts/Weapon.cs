using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] WeaponSO weaponSO;
	public WeaponSO WeaponSO { get => weaponSO; }

	ParticleSystem muzzleFlash;
	Animator animator;
	float timeSinceLastShot;

	const string SHOOT_ANIMATION = "Shoot";



	void Awake ()
	{
		animator = GetComponentInParent<Animator>();
	}


	void Start ()
	{
		muzzleFlash = GetComponentInChildren<ParticleSystem>();
		timeSinceLastShot = WeaponSO.FireRate;
	}


	void Update ()
	{
		timeSinceLastShot += Time.deltaTime;
	}


	public void Shoot ()
	{
		if (timeSinceLastShot < WeaponSO.FireRate) return;

		timeSinceLastShot = 0;

		muzzleFlash.Play();
		animator.Play(SHOOT_ANIMATION, 0, 0);
		
		if ( Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity) )
		{
			Instantiate(WeaponSO.HitVFXPrefab, hit.point, Quaternion.identity);	// Shot hit VFX;
			EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();	// Check if hit an enemy.
			enemyHealth?.TakeDamage(WeaponSO.Damage);	// Damage enemy if hit.
		}
	}
}
