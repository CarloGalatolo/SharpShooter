using UnityEngine;

public class Pickup : MonoBehaviour
{
	[SerializeField] WeaponSO weaponSO;

	const string PLAYER_TAG = "Player";



	void OnTriggerEnter (Collider other)
	{
		if (!other.CompareTag(PLAYER_TAG)) return;

		ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
		activeWeapon.SwitchWeapon(weaponSO);
		Destroy(this.gameObject);
	}
}
