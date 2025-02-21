using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
	[SerializeField] float floatingPhase = 2;
	[SerializeField] float floatingAmplitude = 0.001f;
	[SerializeField] float rotationSpeed = 50;

	const string PLAYER_TAG = "Player";



	void Update ()
	{
		transform.Translate(0, Mathf.Sin(Time.time * floatingPhase) * floatingAmplitude, 0);	// Float
		transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
	}


	void OnTriggerEnter (Collider other)
	{
		if (!other.CompareTag(PLAYER_TAG)) return;

		ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
		OnPickup(activeWeapon);
	}


	protected abstract void OnPickup (ActiveWeapon activeWeapon);
}
