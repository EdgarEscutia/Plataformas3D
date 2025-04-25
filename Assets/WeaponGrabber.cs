using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponGrabber : MonoBehaviour
{
    [SerializeField] Rig leftArmRig;
    [SerializeField] Rig rightArmRig;

    [SerializeField] RigTransform leftTarget;
    [SerializeField] RigTransform rightTarget;

    [SerializeField] RigTransform leftHint;
    [SerializeField] RigTransform rightHint;

    WeaponManager weaponManager;

    Transform currentGrabPointsParent;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void OnEnable()
    {
        weaponManager.onWeaponChange.AddListener(OnWeaponChange);
    }

    private void OnWeaponChange(Weapon weapon)
    {
        currentGrabPointsParent = weapon.grabPointParent;

        leftArmRig.weight = currentGrabPointsParent != null ? 1f : 0f;
        rightArmRig.weight = currentGrabPointsParent != null ? 1f : 0f;
    }

    private void OnDisable()
    {
        weaponManager.onWeaponChange.RemoveListener(OnWeaponChange);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGrabPointsParent != null)
        {
            MoveRigTransformToGrabPointChildren(leftTarget, currentGrabPointsParent.GetChild(0).transform); 
            MoveRigTransformToGrabPointChildren(rightTarget, currentGrabPointsParent.GetChild(1).transform); 
            MoveRigTransformToGrabPointChildren(leftHint, currentGrabPointsParent.GetChild(2).transform); 
            MoveRigTransformToGrabPointChildren(rightHint, currentGrabPointsParent.GetChild(3).transform); 
            
        }
    }

    private void MoveRigTransformToGrabPointChildren(RigTransform rt, Transform t)
    {
        rt.transform.position = t.position;
        rt.transform.rotation = t.rotation;
    }
}
