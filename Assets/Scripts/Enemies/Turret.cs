		using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
	[SerializeField] GameObject projectilePrefab;
	[SerializeField] Transform turretHead;
	[SerializeField] Transform playerTargetPoint;
	[SerializeField] Transform projectileSpawnPoint;
	[SerializeField] float FireRateSeconds = 2;
	[SerializeField] uint damage = 2;

	PlayerHealth player;



	void Start ()
	{
		player = FindFirstObjectByType<PlayerHealth>();

		StartCoroutine( FireRoutine() );
	}


	void Update ()
	{
		if (player)
		{
			turretHead.LookAt(playerTargetPoint.position);
		}
	}


	IEnumerator FireRoutine ()
	{
		while (player)
		{
			yield return new WaitForSeconds(FireRateSeconds);
			Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
			projectile.transform.LookAt(playerTargetPoint);
			projectile.Init(damage);
		}
	}
}
