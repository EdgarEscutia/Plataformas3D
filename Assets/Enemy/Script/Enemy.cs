using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] Transform target;

    NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //Vector3 playerPosition = PlayerController.instance.transform.position;

        //if(Vector3.Distance(playerPosition, transform.position))


            agent.destination = target.position;
    }
}
