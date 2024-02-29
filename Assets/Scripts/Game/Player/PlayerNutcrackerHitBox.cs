using UnityEngine;

public class PlayerNutcrackerHitBox : HitBoxBase
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
