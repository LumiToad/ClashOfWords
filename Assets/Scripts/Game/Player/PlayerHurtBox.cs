using UnityEngine;

public class PlayerHurtBox : MonoBehaviour, IHitable
{
    private Player player;
    private PlayerActions playerActions;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerActions = GetComponent<PlayerActions>();
    }

    private void OnHit(HitBoxBase hitBox)
    {
        if (hitBox == null) return;
        
        
        if (hitBox is PlayerTailHitBox)
        {
            HitByPlayerTailHitBox(hitBox as PlayerTailHitBox);
            return;
        }

        if (hitBox is ItemHitBox)
        {
            HitByItem(hitBox as ItemHitBox);
            return;
        }
    }

    private void HitByPlayerTailHitBox(PlayerTailHitBox playerTailHitBox)
    {
        if (player.ColorType != playerTailHitBox.Player.ColorType)
        {
            playerActions.TakeHit();
        }
    }

    private void HitByItem(ItemHitBox itemHitBox)
    {
        playerActions.TakeHit();
    }

    #region IHitable
    public void OnHitReceived(HitBoxBase hitBox)
    {
        OnHit(hitBox);
    }

    #endregion IHitable
}
