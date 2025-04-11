using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Sight : MonoBehaviour
{
    [SerializeField] float range = 15f;
    [SerializeField] float width = 10f;
    [SerializeField] float height = 7f;

    [SerializeField] LayerMask visibleLayersMask = Physics.DefaultRaycastLayers;
    [SerializeField] LayerMask OcludingLayersMask = Physics.DefaultRaycastLayers;

    public List<ITargeteable> targeteables = new();
    [SerializeField] ITargeteable parentTargeteable;

    private void Awake()
    {
        parentTargeteable= GetComponentInParent<ITargeteable>();
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(
            transform.position + (transform.forward * (range / 2f)),
             (Vector3.forward * (range/ 2f)) + 
             (Vector3.right * (width / 2f))+
             (Vector3.up * (height/2f)),
            transform.rotation,
            visibleLayersMask);

        targeteables.Clear();

        foreach (Collider c in colliders)
        {
            ITargeteable targeteable = c.GetComponent<ITargeteable>();

            if (targeteable != null)
            {
                if (IsVisibleBecauseFaction(targeteable))
                {
                    bool hasLineOfSight = true;
                    if (Physics.Raycast(transform.position, c.transform.position, out RaycastHit hit, range, OcludingLayersMask))
                    {
                        hasLineOfSight = hit.collider == c;
                    }
                    if (hasLineOfSight) {targeteables.Add(targeteable); }
                }
            }
        }
        
    }

    private static bool IsVisibleBecauseFaction(ITargeteable targeteable)
    {
        bool isVisibleBecauseFaction = false;

        switch (targeteable.GetFaction())
        {
            case ITargeteable.Faction.Player:
                break;

            case ITargeteable.Faction.Enemy:
                isVisibleBecauseFaction = targeteable.GetFaction() != ITargeteable.Faction.Enemy;
                break;
            case ITargeteable.Faction.Ally:
                isVisibleBecauseFaction = targeteable.GetFaction() != ITargeteable.Faction.Enemy;
                break;
        }
        return isVisibleBecauseFaction;
    }

    public ITargeteable GetClosestTarget()
    {
        //Debug.Log((targeteables.Count > 0) ? targeteables[0] : null);
        return (targeteables.Count > 0) ? targeteables[0] : null;
    }
}
