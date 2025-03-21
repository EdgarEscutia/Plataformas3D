using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] InputActionReference shot;
    [SerializeField] InputActionReference Weapon1;
    [SerializeField] InputActionReference Weapon2;
    [SerializeField] InputActionReference Weapon3;
    [SerializeField] InputActionReference Weapon4;
    [SerializeField] InputActionReference Weapon5;
    [SerializeField] InputActionReference CycleWeapon;

    [SerializeField] WeaponManager weaponManager;

    private void OnEnable()
    {
        shot.action.Enable();
        Weapon1.action.Enable();
        Weapon2.action.Enable();
        Weapon3.action.Enable();
        Weapon4.action.Enable();
        Weapon5.action.Enable();
        CycleWeapon.action.Enable();
    }

    private void OnDisable()
    {
        shot.action.Disable();
        Weapon1.action.Disable();
        Weapon2.action.Disable();            
        Weapon3.action.Disable();          
        Weapon4.action.Disable();         
        Weapon5.action.Disable();         
            
        CycleWeapon.action.Disable();
    }

    private void Update()
    {
        UpdateShooting();
        SelectWeaponByNumber();
        SelectWeaponByCycle();
    }

    private void UpdateShooting()
    {
        if (weaponManager.HasSelectedWeapon())
        {
            Weapon currentWeapon = weaponManager.GetCurrentWeapon();
            switch (currentWeapon.weaponType)
            {
                case Weapon.WeaponType.ShotByShot:
                    if (shot.action.WasPressedThisFrame())
                    {
                        currentWeapon.Shot();
                    }
                    break;
                case Weapon.WeaponType.ContinousShot:
                    if (shot.action.WasPressedThisFrame())
                    {
                        currentWeapon.StartShooting();
                    }
                    else if (shot.action.WasReleasedThisFrame())
                    {
                        currentWeapon.StopShooting();
                    }
                    break;
            }
        }
    }
    

    private void SelectWeaponByNumber()
    {
        int weaponToSelect = -2;

        if(Weapon1.action.WasPressedThisFrame())
        {
            weaponToSelect = 0;
        }
        else if (Weapon2.action.WasPressedThisFrame())
        {
            weaponToSelect = 1;
        }
        else if (Weapon3.action.WasPressedThisFrame())
        {
            weaponToSelect = 2;
        }
        else if (Weapon4.action.WasPressedThisFrame())
        {
            weaponToSelect = 3;
        }
        else if (Weapon5.action.WasPressedThisFrame())
        {
            weaponToSelect = 4;
        }   

        if (weaponToSelect != -2)
        {
            weaponManager.SelectWeapon(weaponToSelect);
        }
    }
    private void SelectWeaponByCycle()
    {
        Vector2 scrollDelta = CycleWeapon.action.ReadValue<Vector2>();

        if(scrollDelta.y > 0f)
        {
            weaponManager.SelectNextWeapon();
        }
        else if(scrollDelta.y > 0f)
        {
            weaponManager.SelectPreviousWeapon();
        }
    }

}
