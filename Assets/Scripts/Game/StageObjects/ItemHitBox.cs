using System;
using UnityEngine;

public class ItemHitBox : HitBoxBase
{
    private Rigidbody rb;
    public Rigidbody Rigidbody { get {  return rb; } set { rb = value; } }

    private float minVelocityToHit = 0.1f;
    public float MinVelocityToHit { get { return minVelocityToHit; } set {  minVelocityToHit = value; } }

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();

        DisableHitBox();
    }

    private void Update()
    {
        if (rb.velocity.magnitude < minVelocityToHit)
        {
            DisableHitBox();
        }
    }

    public void SetHittingStateActive(IHitable[] hitables)
    {
        EnableHitBox();
        foreach (IHitable hitable in hitables)
        {
            if (!hitablesExcludeOnce.Contains(hitable))
            {
                AddHitableToExcludeList(hitable);
            }
        }
    }
}
