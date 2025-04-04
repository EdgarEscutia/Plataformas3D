using UnityEngine;

public class StateGoToLastTargetPosition : BaseState
{
    [SerializeField] float reachingThreshold = 1.5f;
    protected override void Update()
    {
        base.Update();
        enemy.GetAgent().SetDestination(enemy.GetLastTargetPosition());
        

        if(Vector3.Distance(transform.position, enemy.GetLastTargetPosition()) < reachingThreshold)
        {
            enemy.NotifyLastTargetPositionReached();
        }
    }
}
