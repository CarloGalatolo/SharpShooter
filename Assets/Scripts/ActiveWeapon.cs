using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
	[SerializeField] CinemachineVirtualCamera playerFollowCamera;
	[SerializeField] GameObject zoomVignette;

	StarterAssetsInputs starterAssetsInputs;
	FirstPersonController firstPersonController;
	Weapon currentWeapon;
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
		currentWeapon = GetComponentInChildren<Weapon>();
	}


	void Update()
	{
		HandleShoot();
		HandleZoom();
	}


	public void SwitchWeapon (WeaponSO weaponSO)
	{
		// Debug.Log(weaponSO.name);
		if (currentWeapon)
		{
			Destroy(currentWeapon.gameObject);
		}

		currentWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
	}


	private void HandleShoot ()
	{
		if (!starterAssetsInputs.shoot) return;
		if (!currentWeapon) return;

		if (!currentWeapon.WeaponSO.IsAutomatic)
		{
			starterAssetsInputs.ShootInput(false);	// Reset input value.
		}
		
		currentWeapon.Shoot();
	}


	void HandleZoom ()
	{
		if (!currentWeapon) return;
		if (!currentWeapon.WeaponSO.CanZoom) return;

		if (starterAssetsInputs.zoom)
		{
			playerFollowCamera.m_Lens.FieldOfView = currentWeapon.WeaponSO.ZoomAmount;
			zoomVignette.SetActive(true);
			firstPersonController.RotationSpeed = currentWeapon.WeaponSO.ZoomRotationSpeed;
		}
		else
		{
			playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
			zoomVignette.SetActive(false);
			firstPersonController.RotationSpeed = defaultRotationSpeed;
		}
	}
}
