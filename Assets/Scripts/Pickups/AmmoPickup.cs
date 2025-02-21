using Unity.VisualScripting;
using UnityEngine;

public class AmmoPickup : Pickup
{
	[SerializeField] int ammoAmount = 100;



	protected override void OnPickup(ActiveWeapon activeWeapon)
	{
		if ( activeWeapon.CurrentWeapon )
		{
			activeWeapon.CurrentWeapon.AdjustAmmo(ammoAmount);
			Destroy(this.gameObject);
		}
	}
}
