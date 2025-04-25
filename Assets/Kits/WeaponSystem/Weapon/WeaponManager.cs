using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public int initialWeaponToSelect;

    [SerializeField] public UnityEvent<Weapon> onWeaponChange;


    [Header("Debug")]
    [SerializeField] public bool debugSelectNextWeapon;
    [SerializeField] public bool debugSelectPrevWeapon;
    [SerializeField] public bool debugShot;
    [SerializeField] public bool debugStartShooting;
    [SerializeField]  public bool debugStopShooting;

    Weapon[] weapons;
    public int currentWeaponIndex = -1;


    //List<Weapon> weapons = new List<Weapon>();

    private void Awake()
    {
        weapons = GetComponentsInChildren<Weapon>();

        //for (int i = 0; i < weapons.Length; i++) 
        //{
        //    //weapons.Add(transform.GetComponentInChildren<Weapon>());
        //}
    }

    private void Start()
    {
        foreach (Weapon weapon in weapons) 
        { 
            weapon.gameObject.SetActive(false);
        }
        SelectWeapon(initialWeaponToSelect);
    }

    private void OnValidate()
    {
        if (debugSelectNextWeapon)
        {
            debugSelectNextWeapon = false;
            SelectNextWeapon();
        }

        if (debugSelectPrevWeapon)
        {
            debugSelectPrevWeapon = false;
            SelectPreviousWeapon();
        }

        if (debugShot)
        {
            debugShot = false;
            Shot();
        }
        if (debugStartShooting)
        {
            debugStartShooting = false;
            StartContinuosShooting();
        }
        if (debugStopShooting)
        {
            debugStopShooting = false;
            StopContinuosShooting();
        }
    }

    public void SelectWeapon(int weaponIndex)
    {
        if((weaponIndex <-1))
        {
            weaponIndex = weapons.Length - 1;
        }
        if(weaponIndex >= weapons.Length)
        {
            weaponIndex = -1;
        }

        if(currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].NotifyDeselected();
            weapons[currentWeaponIndex].gameObject.SetActive(false);
        }
        currentWeaponIndex = weaponIndex;

        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].gameObject.SetActive(true);
            weapons[currentWeaponIndex].NotifySelected();

        }
        onWeaponChange.Invoke(currentWeaponIndex != -1 ? weapons[currentWeaponIndex] : null);
    }

    public bool HasSelectedWeapon()
    {
        return currentWeaponIndex != -1;
    }
    public bool HasSelectedWeaponIsShotByShot()
    {
        return currentWeaponIndex != -1 ? 
            weapons[currentWeaponIndex].weaponType == Weapon.WeaponType.ShotByShot : false;
    }

    public void Shot()
    {
        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].Shot();
        }
    }

    public void StartContinuosShooting()
    {
        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].StartShooting();
        }
    }

    public void StopContinuosShooting()
    {
        if (currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].StopShooting();
        }
    }

    public void SelectNextWeapon()
    {
        SelectWeapon(currentWeaponIndex + 1);
    }



    public void SelectPreviousWeapon()
    {
        SelectWeapon(currentWeaponIndex - 1);
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeaponIndex == -1 ? null: weapons[currentWeaponIndex];
    }
}