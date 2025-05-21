using UnityEngine;

public interface IDamageable
{
    public void NotifyDamageReceived(float damageAmount);


    public bool HasTakenDamage { get; set;}



}
