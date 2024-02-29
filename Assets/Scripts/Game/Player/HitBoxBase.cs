using System.Collections.Generic;
using UnityEngine;

public class HitBoxBase : MonoBehaviour
{
    protected SphereCollider sphereCollider;

    protected List<IHitable> hitablesExcludeOnce = new List<IHitable>();

    private void Start()
    {
        if (sphereCollider == null) return;
      
        Setup();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == GetComponentInParent<SphereCollider>()) return;
        
        SendHit(other);
    }

    protected virtual void SendHit(Collider other)
    {
        IHitable[] hitables = other.GetComponents<IHitable>();

        foreach (IHitable hitable in hitables)
        {
            if (hitable == null) return;
            if (hitablesExcludeOnce.Contains(hitable)) return;

            hitable.OnHitReceived(this);
            hitablesExcludeOnce.Add(hitable);
        }
    }

    private void Setup()
    {
        DisableHitBox();
    }

    public void EnableHitBox()
    {
        sphereCollider.enabled = true;
    }

    public void DisableHitBox()
    {
        sphereCollider.enabled = false;
        hitablesExcludeOnce.Clear();
    }

    public void AddHitableToExcludeList(IHitable hitable)
    {
        hitablesExcludeOnce.Add(hitable);
    }
}
