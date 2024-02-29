using System.Collections;
using UnityEngine;

public class Kickable : MonoBehaviour, IHitable
{
    [SerializeField]
    private float minVelocityToHit = 7.5f;

    private Rigidbody rb;

    private ItemHitBox itemHitBox;

    public float powerModifier = 0.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        itemHitBox = GetComponentInChildren<ItemHitBox>();

        itemHitBox.Rigidbody = rb;
        itemHitBox.MinVelocityToHit = minVelocityToHit;
    }

    private void HitByPlayerTail(PlayerTailHitBox playerTailHitBox)
    {
        var playerPosition = playerTailHitBox.gameObject.transform.position;
        Vector3 direction = (transform.position - playerPosition);
        direction.y = 0.0f;

        ApplyForce(direction, playerTailHitBox.Player.KickForce);
    }

    private void ApplyForce(Vector3 direction, float power)
    {
        Vector3 force = direction * power;
        if (powerModifier > 0.0f)
        {
            force *= powerModifier;
        }
        
        rb.AddForce(force);
    }

    private void OnHit(HitBoxBase hitBox)
    {
        if (hitBox is PlayerTailHitBox)
        {
            HitByPlayerTail(hitBox as PlayerTailHitBox);
            Game.Instance.SFXPlayer.PlaySFXByType(SFXClip.Kick, gameObject);

            IHitable[] hitables = hitBox.gameObject.GetComponentsInParent<IHitable>();
            itemHitBox.DisableHitBox();
            itemHitBox.SetHittingStateActive(hitables);
        }
    }

    #region IHitable
    public void OnHitReceived(HitBoxBase hitBox)
    {
        OnHit(hitBox);
    }

    public void ResetVelocity()
    {
        rb.isKinematic = true;
        StartCoroutine(ResetVelocityInternal());
    }

    #endregion IHitable

    private IEnumerator ResetVelocityInternal()
    {
        yield return new WaitForEndOfFrame();
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
