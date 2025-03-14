using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] float wanderingRadius = 5f;
    [SerializeField] float reachingDistance = 1.5f;

    [SerializeField] float detectionDistance = 3f;


    NavMeshAgent agent;

    Vector3 homeOrigin;
    Vector3 wanderPosition;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();

        homeOrigin = transform.position;
        SelectedWanderPosition();
    }

    private void Update()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;

        if (PlayerController.instance.gameObject.activeSelf &&
            (Vector3.Distance(playerPosition, transform.position) < detectionDistance))
        {
            agent.SetDestination(playerPosition);
        }
        else
        {
            agent.SetDestination(wanderPosition);

            if (Vector3.Distance(transform.position, wanderPosition) < reachingDistance)
            {
                SelectedWanderPosition();
            }
        }

        UpdateAnimation();

    }

    private void SelectedWanderPosition()
    {
        Vector2 positionXY = Random.insideUnitCircle * wanderingRadius;
        Vector3 positionXZ = new Vector3(positionXY.x, 0f, positionXY.y);
        wanderPosition = homeOrigin + positionXZ;
    }

    protected override float GetCurrentVerticalSpeed()
    {
        return 0f;
    }

    protected override float GetJumpSpeed()
    {
        return 0f;
    }

    protected override bool IsRunning()
    {
        return true;
    }

    protected override bool IsGrounded()
    {
        return true;
    }

    protected override Vector3 GetLastNormalizedVelocity()
    {
        return agent.velocity.normalized;
    }
}
