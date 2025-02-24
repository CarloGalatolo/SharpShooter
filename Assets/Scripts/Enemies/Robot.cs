using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
	NavMeshAgent navMeshAgent;
	FirstPersonController player;

	const string PLAYER_TAG = "Player";

	void Awake ()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();
	}


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start ()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }


    // Update is called once per frame
    void Update()
    {
		if (!player) return;
		
        navMeshAgent.SetDestination(player.transform.position);
    }


	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag(PLAYER_TAG))
		{
			EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
			enemyHealth.SelfDestruct();
		}
	}
}
