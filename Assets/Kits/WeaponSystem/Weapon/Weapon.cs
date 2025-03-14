using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    enum type {ShotByShot, ContinousShot}

    List<BarrelBase> barrelBase = new List<BarrelBase>();

    private void Awake()
    {
        for (int i = 0; i < 10; i++) 
        {
            barrelBase.Add(transform.GetComponentInChildren<BarrelBase>());
        }
    }
    public virtual void ShootOnce()
    {
        for (int i = 0; i < barrelBase.Count; i++) 
        { 
            
        }
    }
    public void StartShooting()
    {
        for (int i = 0; i < barrelBase.Count; i++)
        {

        }
    }

    public  void StopShooting()
    {
        for (int i = 0; i < barrelBase.Count; i++)
        {

        }
    }

    public void NotifySelected()
    {

    }

   

     public void NotifyDesSelected()
    {

    }


}
