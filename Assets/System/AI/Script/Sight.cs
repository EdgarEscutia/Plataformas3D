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

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(
            transform.position + (transform.forward * (range / 2f)),
        (Vector3.forward * (range/ 2f)) + 
        (Vector3.right * (width / 2f))+
        (Vector3.up * (height/2f)),
            transform.rotation,
            visibleLayersMask);


        List<Collider> colliderInLineOfSight = new();

        foreach (Collider c in colliders)
        {
            bool hasLineOfSight = true;
            if(Physics.Raycast(transform.position, c.transform.position,out RaycastHit hit,range, OcludingLayersMask))
            {
                hasLineOfSight = hit.collider == c;
            }

            if(hasLineOfSight) { colliderInLineOfSight.Add(c); }
        }



        
    }
}
