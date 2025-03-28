using UnityEngine;

public class StatePatrol : BaseState
{
    [SerializeField] Transform patrolPointsParent;
    [SerializeField] int startingPatrolPointIndex = 0;
    [SerializeField] float reachThreshold = 1.5f;

    int currentPatrolPoint;

    protected override void Awake()
    {
        base.Awake();
        currentPatrolPoint = startingPatrolPointIndex;
    }

    protected override void Update()
    {
        base.Update();
        Vector3 patrolPointPosition = patrolPointsParent.GetChild(startingPatrolPointIndex).position;
    
        if(Vector3.Distance(transform.position, patrolPointPosition) < reachThreshold)
        {
            currentPatrolPoint++;
            if(currentPatrolPoint >= patrolPointsParent.childCount)
            {
                currentPatrolPoint = 0;
            }
        }
        enemy.GetAgent().destination = patrolPointPosition;
    }
}
