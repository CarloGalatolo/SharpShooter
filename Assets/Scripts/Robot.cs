using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
	NavMeshAgent navMeshAgent;
	FirstPersonController player;

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
        navMeshAgent.SetDestination(player.transform.position);
    }
}
