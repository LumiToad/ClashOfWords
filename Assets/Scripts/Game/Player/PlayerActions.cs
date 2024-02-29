using UnityEngine;

public class PlayerActions : MonoBehaviour, IAnimationEventReceiver
{
    [SerializeField]
    private float speed = 8.5f;

    [SerializeField]
    private float angularSpeed = 8.5f;

    private Rigidbody rb;
    private PlayerAttack playerAttack;

    private bool isMovementEnabled = true;
    private bool IsMovementEnabled 
    { 
        get => isMovementEnabled;
        set
        {
            isMovementEnabled = value;
        }
    }

    private bool isAttackingEnabled = true;
    private bool IsAttackingEnabled 
    { 
        get => isAttackingEnabled;
        set => isAttackingEnabled = value; 
    }

    private bool canTakeHit = true;
    public bool CanTakeHit
    {
        get => canTakeHit;
        set => canTakeHit = value;
    }

    private bool isPlayingTriggerAnimation = false;

    private PlayerAnimations animations;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        playerAttack = GetComponentInChildren<PlayerAttack>();
        animations = GetComponentInChildren<PlayerAnimations>();
        SubscribeAnimationTriggers();
        animations.Player = gameObject;
    }

    private void OnDisable()
    {
        UnsubscribeAnimationTriggers();
    }

    public void MoveAction(Vector3 direction)
    {
        if (!isMovementEnabled) return;
        if (animations == null) return;

        direction.y = 0;
        rb.velocity = direction * speed;

        bool isWalking = direction != Vector3.zero;
        animations.PlayWalking(isWalking);
        LerpLookAt(direction, isWalking);
    }

    private void LerpLookAt(Vector3 direction, bool isWalking)
    {
        if (!isWalking) return;
        if (isPlayingTriggerAnimation) return;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, angularSpeed * Time.deltaTime);
    }

    public void TakeHit()
    {
        if (!CanTakeHit) return;
        animations.PlayStunned();
        Game.Instance.SFXPlayer.PlaySFXByType(SFXClip.Stunned, gameObject);
    }

    public void TailAction()
    {
        if (!IsAttackingEnabled) return;
        
        animations.PlayAttack();
        Game.Instance.SFXPlayer.PlaySFXByType(SFXClip.TailSlap, gameObject);
    }

    public void NutcrackerAction()
    {
        if (!IsAttackingEnabled) return;

        animations.PlayNutcracker();
    }

    #region AnimationEvents

    private void OnNutcrackerActionStart()
    {
        IsMovementEnabled = false;
        IsAttackingEnabled = false;
        isPlayingTriggerAnimation = true;
    }

    private void OnNutcrackerActionEnd()
    {
        IsMovementEnabled = true;
        IsAttackingEnabled = true;
        isPlayingTriggerAnimation = false;
    }

    private void OnNutcrackerHitboxEnable()
    {
        playerAttack.OnNutcrackerHitStart();

    }

    private void OnNutcrackerHitboxDisable()
    {
        playerAttack.OnNutcrackerHitEnd();
    }

    private void OnTailActionStart()
    {
        playerAttack.OnTailHitStart();

        IsAttackingEnabled = false;
        isPlayingTriggerAnimation = true;
    }

    private void OnTailActionEnd()
    {
        playerAttack.OnTailHitEnd();

        IsAttackingEnabled = true;
        isPlayingTriggerAnimation = false;
    }

    private void OnStunnedActionStart()
    {
        IsMovementEnabled = false;
        IsAttackingEnabled = false;
        CanTakeHit = false;
        isPlayingTriggerAnimation = true;
    }

    private void OnStunnedActionEnd()
    {
        IsMovementEnabled = true;
        IsAttackingEnabled = true;
        CanTakeHit = true;
        isPlayingTriggerAnimation = false;
    }

    #endregion AnimationEvents

    #region IAnimationEventReceiver
    public void AnimationStartReceived(string animationName)
    {
        switch (animationName)
        {
            case ("Attack"):
                OnTailActionStart();
                break;
            case ("Nutcracker"):
                OnNutcrackerActionStart();
                break;
            case ("Stunned"):
                OnStunnedActionStart();
                break;
        }
    }

    public void AnimationEndReceived(string animationName)
    {
        switch (animationName) 
        {
            case ("Attack"):
                OnTailActionEnd();
                break;
            case ("Nutcracker"):
                OnNutcrackerActionEnd();
                break;
            case ("Stunned"):
                OnStunnedActionEnd();
                break;
        }
    }

    public void AnimationEventReceived(string eventName)
    {
        switch (eventName)
        {
            case ("NutcrackerHitBoxEnable"):
                OnNutcrackerHitboxEnable();
                break;
            case ("NutcrackerHitBoxDisable"):
                OnNutcrackerHitboxDisable();
                break;
        }
    }

    public void SubscribeAnimationTriggers()
    {
        animations.AnimationStart += AnimationStartReceived;
        animations.AnimationEnd += AnimationEndReceived;
        animations.AnimationEvent += AnimationEventReceived;
    }

    public void UnsubscribeAnimationTriggers()
    {
        animations.AnimationStart -= AnimationStartReceived;
        animations.AnimationEnd -= AnimationEndReceived;
        animations.AnimationEvent -= AnimationEventReceived;
    }

    #endregion IAnimationEventReceiver
}
