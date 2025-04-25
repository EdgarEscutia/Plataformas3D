using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType {ShotByShot, ContinousShot}

    //List<BarrelBase> barrelBase = new List<BarrelBase>();

    [SerializeField] public WeaponType weaponType;

    [SerializeField] public Transform grabPointParent;

    [Header("Debug")]
    public bool debugShot;
    public bool debugStartShot;
    public bool debugStopShot;

    public BarrelBase[] allBarrels;

    private void Awake()
    {
        allBarrels = GetComponentsInChildren<BarrelBase>();

        //for (int i = 0; i < 10; i++) 
        //{
        //    barrelBase.Add(transform.GetComponentInChildren<BarrelBase>());
        //}
    }

    private void OnValidate()
    {
        if (debugShot) { 
            debugShot = false;
            Shot();
        }
        if(debugStartShot)
        {
            debugStartShot = false;
            StartShooting();

        }
        if(debugStopShot)
        {
            debugStopShot = false;
            StopShooting();
        }
    }
    public void Shot()
    {
        foreach (BarrelBase barrel in allBarrels) 
        { 
            barrel.ShootOnce();
        }
    }
    public void StartShooting()
    {
        foreach (BarrelBase barrel in allBarrels)
        {
            barrel.StartShooting();
        }
    }

    public  void StopShooting()
    {
        foreach (BarrelBase barrel in allBarrels)
        {
            barrel.StopShooting();
        }
    }

    public void NotifySelected()
    {

    }

    public void NotifyDeselected()
    {
        if(weaponType == WeaponType.ContinousShot)
        {
            StopShooting();
        }
    }


}
