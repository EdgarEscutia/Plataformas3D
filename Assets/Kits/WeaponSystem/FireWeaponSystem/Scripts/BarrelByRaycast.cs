using UnityEngine;

public class BarrelByRaycast : BarrelBase, IHitter
{
    [SerializeField] private float range = 15f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;
    [SerializeField] float damage = 0.25f;

    public override void ShootOnce()
    {
        if(Physics.Raycast(transform.position, transform.forward,out RaycastHit hit, range, layerMask))
        {
            //HurtCollider hurtCollider = hit.collider.GetComponent<HumanBodyBones>();
            //hurtCollider.NotifyHit(this);
        }
    }

    float IHitter.GetDamage()
    {
        return damage;
    }

    Transform IHitter.GetTransform()
    {
        return transform;
    }
}
