using UnityEngine;

public class StateChasing : BaseState
{
    protected override void Update()
    {
        base.Update();
        Transform target = enemy.GetTarget();
        enemy.GetAgent().SetDestination(target.position);
    }
}
