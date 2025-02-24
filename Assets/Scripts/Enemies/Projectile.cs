using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] GameObject hitVFX;
	[SerializeField] float speed = 30;

	Rigidbody rigidBody;
	uint damage;



	void Awake ()
	{
		rigidBody = GetComponent<Rigidbody>();
	}


	void Start ()
	{
		rigidBody.linearVelocity = transform.forward * speed;
	}


	public void Init (uint damage)
	{
		this.damage = damage;
	}


	void OnTriggerEnter (Collider other)
	{
		PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
		playerHealth?.TakeDamage(damage);

		Instantiate(hitVFX, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
