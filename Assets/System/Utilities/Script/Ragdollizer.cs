using UnityEngine;

public class Ragdollizer : MonoBehaviour
{
    [SerializeField] bool ragdollizedOnAwake = false;

    Rigidbody[] rigidbodies;
    Collider[] colliders;
    Animator animator;


    [Header("Debug")]
    [SerializeField] bool debugRagdolize;
    [SerializeField] bool debugDeRagdolize;

    private void OnValidate()
    {
        if(debugRagdolize)
        {
            debugRagdolize = false;
            Ragdollize();
        }
        if (debugDeRagdolize) 
        {
            debugDeRagdolize= false;
            DeRagdollize();
        }
    }
    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();

        if (ragdollizedOnAwake)
        {
            animator.enabled = false;

        }
        else 
        { 
            foreach(Rigidbody b in rigidbodies) { b.isKinematic = true; }    
            foreach(Collider c in colliders) { c.enabled = false; }
        }
    }

    public void Ragdollize()
    {
        foreach (Rigidbody b in rigidbodies) { b.isKinematic = false; }
        foreach (Collider c in colliders) { c.enabled = true; }
        animator.enabled = false;
    }

    public void DeRagdollize()
    {
        foreach (Rigidbody b in rigidbodies) { b.isKinematic = true; }
        foreach (Collider c in colliders) { c.enabled = false; }
        animator.enabled = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
