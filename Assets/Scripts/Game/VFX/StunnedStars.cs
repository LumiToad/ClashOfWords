using UnityEngine;

public class StunnedStars : MonoBehaviour, IAnimationEventReceiver
{
    private PlayerAnimations playerAnimations;
    public PlayerAnimations PlayerAnimations 
    { 
        get { return playerAnimations; }
        set 
        { 
            playerAnimations = value;
            SubscribeAnimationTriggers();
        }
    }

    private void Awake()
    {
        Show(false);
    }

    private void OnDisable()
    {
        UnsubscribeAnimationTriggers();
    }

    private void Show(bool value)
    {
        foreach (Transform transform in GetComponentsInChildren<Transform>(true))
        {
            if (transform.gameObject == gameObject) continue;
            transform.gameObject.SetActive(value);
        }
    }

    #region IAnimationEventReceiver

    public void AnimationEndReceived(string animationName)
    {
        if (animationName == "Stunned")
        {
            Show(false);
        }
    }

    public void AnimationEventReceived(string eventName)
    {
        
    }

    public void AnimationStartReceived(string animationName)
    {
        if (animationName == "Stunned")
        {
            Show(true);
        }
    }

    public void SubscribeAnimationTriggers()
    {
        PlayerAnimations.AnimationStart += AnimationStartReceived;
        PlayerAnimations.AnimationEnd += AnimationEndReceived;
        PlayerAnimations.AnimationEvent += AnimationEventReceived;
    }

    public void UnsubscribeAnimationTriggers()
    {
        PlayerAnimations.AnimationStart -= AnimationStartReceived;
        PlayerAnimations.AnimationEnd -= AnimationEndReceived;
        PlayerAnimations.AnimationEvent -= AnimationEventReceived;
    }

    #endregion IAnimationEventReceiver
}
