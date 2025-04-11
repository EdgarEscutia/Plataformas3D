using UnityEngine;

public class StatePatrol : BaseState
{
    [SerializeField] Transform patrolPointsParent;
    [SerializeField] int startingPatrolPointIndex = 0;
    [SerializeField] float reachThreshold = 1.5f;

    int currentPatrolPointndex;

    protected override void Awake()
    {
        base.Awake();
        currentPatrolPointndex = startingPatrolPointIndex;
    }

    protected override void Update()
    {
        base.Update();
        Vector3 patrolPointPosition = patrolPointsParent.GetChild(currentPatrolPointndex).position;
    
        if(Vector3.Distance(transform.position, patrolPointPosition) < reachThreshold)
        {
            Debug.Log("Patrol Point reached");
            currentPatrolPointndex++;
            if(currentPatrolPointndex >= patrolPointsParent.childCount)
            {
                currentPatrolPointndex = 0;
            }
        }
        enemy.GetAgent().destination = patrolPointPosition;
    }
}
