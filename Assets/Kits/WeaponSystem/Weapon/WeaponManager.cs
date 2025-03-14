using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int initialWeaponToSelect;
    public bool debugSelectNext;
    public bool debugSelectPrevWeapon;
    public bool debugShot;
    public bool debugStartShooting;
    public bool debugStopShooting;

    Array[] weaponsArray;
    public int currentWeaponIndex = -1;

    List<Weapon> weapons = new List<Weapon>();

    private void Awake()
    {
        for (int i = 0; i < weaponsArray.Length; i++) 
        {
            weapons.Add(transform.GetComponentInChildren<Weapon>());
        }
    }

    private void Start()
    {

    }
}
