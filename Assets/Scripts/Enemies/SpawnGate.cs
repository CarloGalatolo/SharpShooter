using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
	[SerializeField] float spawnCooldownSeconds = 5;
	[SerializeField] GameObject robotPrefab;
	[SerializeField] Transform spawnPoint;

	PlayerHealth player;
	
	void Start ()
	{
		player = FindFirstObjectByType<PlayerHealth>();

		StartCoroutine( SpawnEnemyRoutine() );
	}

	IEnumerator SpawnEnemyRoutine ()
	{
		while (player)
		{
			Instantiate(robotPrefab, spawnPoint.position, transform.rotation);
			yield return new WaitForSeconds(spawnCooldownSeconds);
		}
	}
}
