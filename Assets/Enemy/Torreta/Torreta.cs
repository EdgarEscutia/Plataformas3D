using UnityEngine;

public class Torreta : MonoBehaviour
{
    Sight sight;
    WeaponManager weaponManager;
    private void Update()
    {
        if (sight != null)
        {
            weaponManager.StartContinuosShooting();
        }
        else
        {
            weaponManager.StopContinuosShooting();
        }
    }
}