using UnityEngine;

public class StateChasing : BaseState
{
    protected override void Update()
    {
        
        base.Update();
        //Debug.Log(enemy);
        Transform target = enemy.GetTarget();
        if (target != null)
        {
            enemy.GetAgent().SetDestination(target.position);
        }
        //Debug.Log(enemy);
        
    }
}