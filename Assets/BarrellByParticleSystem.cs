using UnityEngine;

public class BarrellByParticleSystem : BarrelBase, IHitter
{
    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule em;

    [SerializeField] float damagePerParticle = 0.03f;


    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
       em = particleSystem.emission;
        em.enabled = false;
    }
    public override void ShootOnce()
    {
        particleSystem.Emit(1);
    }
    public override void StartShooting()
    {
        em.enabled= true;
    }

    public override void StopShooting()
    {
        em.enabled = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        //other.GetComponent<HurtCollider>()?.NotifyHit(this);
    }

    public float GetDamage()
    {
        return damagePerParticle;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
