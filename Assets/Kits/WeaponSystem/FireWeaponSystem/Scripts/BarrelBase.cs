using UnityEngine;

public abstract class BarrelBase : MonoBehaviour
{ 
    
    [SerializeField] bool debugShot;
    private void OnValidate()
    {
        if (debugShot)
        {
            debugShot = false;
            ShootOnce();
        }
    }
 
   public virtual void ShootOnce()
   {
        throw new System.Exception("This barrel doesn't suport ShootOnce");
   }
    public virtual void StartShooting()
    {
        throw new System.Exception("This barrel doesn't suport ShootOnce");
    }

    public virtual void StopShooting()
    {
        throw new System.Exception("This barrel doesn't suport ShootOnce");
    }
}
