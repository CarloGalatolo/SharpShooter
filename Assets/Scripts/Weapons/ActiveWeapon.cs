using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
	// Params
	[SerializeField] WeaponSO startingWeapon;
	[SerializeField] CinemachineVirtualCamera playerFollowCamera;
	[SerializeField] Camera weaponCamera;
	[SerializeField] GameObject zoomVignette;
	[SerializeField] TMP_Text ammoText;

	// Cache
	StarterAssetsInputs starterAssetsInputs;
	FirstPersonController firstPersonController;

	// Properties
	public Weapon CurrentWeapon { get; private set; }

	// State
	float defaultFOV;
	float defaultRotationSpeed;


	void Awake()
	{
		starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
		firstPersonController = GetComponentInParent<FirstPersonController>();
		defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
		defaultRotationSpeed = firstPersonController.RotationSpeed;
	}


	void Start()
	{
		CurrentWeapon = GetComponentInChildren<Weapon>();

		if (startingWeapon)	// Allows to start with no weapon equipped.
		{
			SwitchWeapon(startingWeapon);
		}
		else
		{
			ammoText.transform.parent.gameObject.SetActive(false);
		}
	}


	void Update()
	{
		HandleShoot();
		HandleZoom();
	}


	public void SwitchWeapon (WeaponSO weaponSO)
	{
		// Debug.Log(weaponSO.name);
		if (CurrentWeapon)
		{
			Destroy(CurrentWeapon.gameObject);
		}

		CurrentWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
		CurrentWeapon.Init(ammoText);
	}


	private void HandleShoot ()
	{
		if (!starterAssetsInputs.shoot) return;
		if (!CurrentWeapon) return;

		if (!CurrentWeapon.WeaponSO.IsAutomatic)
		{
			starterAssetsInputs.ShootInput(false);	// Reset input value.
		}

		CurrentWeapon.Shoot();
	}


	void HandleZoom ()
	{
		if (!CurrentWeapon) return;
		if (!CurrentWeapon.WeaponSO.CanZoom) return;

		if (starterAssetsInputs.zoom)
		{
			playerFollowCamera.m_Lens.FieldOfView = CurrentWeapon.WeaponSO.ZoomAmount;
			weaponCamera.fieldOfView = CurrentWeapon.WeaponSO.ZoomAmount;
			zoomVignette.SetActive(true);
			firstPersonController.RotationSpeed = CurrentWeapon.WeaponSO.ZoomRotationSpeed;
		}
		else
		{
			playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
			weaponCamera.fieldOfView = defaultFOV;
			zoomVignette.SetActive(false);
			firstPersonController.RotationSpeed = defaultRotationSpeed;
		}
	}
}
