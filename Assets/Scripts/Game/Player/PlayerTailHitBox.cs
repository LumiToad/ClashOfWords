using UnityEngine;

public class PlayerTailHitBox : HitBoxBase
{
    private Player player;
    public Player Player
    { get { return player; } }

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        sphereCollider = GetComponent<SphereCollider>();
    }
}
