using UnityEngine;

public class StateShooting : BaseState
{
    float angularSpeed;

    public override void Init(Enemy enemy)
    {
        base.Init(enemy);
        angularSpeed = enemy.GetAgent().angularSpeed;
        enemy.GetAgent().SetDestination(transform.position);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        enemy.  
            GetWeaponManager().
            StartContinuosShooting();
        //enemy.GetAgent().destination = transform.position;
    }

    protected override void Update()
    {
        base.Update();

        Vector3 desiredDirection = enemy.GetTarget().position - transform.position;
        desiredDirection.y = 0f;

        enemy.GetOrientator().OrientateTo(desiredDirection);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        enemy.GetWeaponManager().StopContinuosShooting();
    }
}
