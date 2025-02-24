using Cinemachine;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] WeaponSO weaponSO;
	[SerializeField] LayerMask interactionLayers;

	ParticleSystem muzzleFlash;
	Animator animator;
	TMP_Text ammoText;	// Inject dependency.
	CinemachineImpulseSource impulseSource;
	float timeSinceLastShot;
	int currentAmmo = 0;


	public WeaponSO WeaponSO { get => weaponSO; }


	const string SHOOT_ANIMATION = "Shoot";



	void Awake ()
	{
		animator = GetComponentInParent<Animator>();
		impulseSource = GetComponent<CinemachineImpulseSource>();
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


	public void Init (TMP_Text ammoText)
	{
		this.ammoText = ammoText;
		AdjustAmmo(WeaponSO.MagazineSize);
	}


	/// <summary>
	/// Fires a shot of the weapon if has bullets and only after the cooldown.
	/// </summary>
	public void Shoot ()
	{
		if (timeSinceLastShot < WeaponSO.FireRate) return;
		if (currentAmmo <= 0)
		{
			// TODO: Play an empty magazine SFX.
			return;
		}

		timeSinceLastShot = 0;

		AdjustAmmo(-1);

		// Cosmetic effects
		muzzleFlash.Play();
		animator.Play(SHOOT_ANIMATION, 0, 0);
		impulseSource.GenerateImpulse();
		
		if ( Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore) )
		{
			Instantiate(WeaponSO.HitVFXPrefab, hit.point, Quaternion.identity);	// Shot hit VFX;
			EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();	// Check if hit an enemy.
			enemyHealth?.TakeDamage(WeaponSO.Damage);	// Damage enemy if hit.
		}
	}
	

	public void AdjustAmmo (int amount)
	{
		currentAmmo = Mathf.Clamp(currentAmmo + amount, 0, WeaponSO.MagazineSize);

		ammoText.transform.parent.gameObject.SetActive(true);
		ammoText.text = currentAmmo.ToString("D2");
	}
}
