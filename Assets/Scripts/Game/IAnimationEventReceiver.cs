public interface IAnimationEventReceiver
{
    public void AnimationStartReceived(string animationName);
    public void AnimationEndReceived(string animationName);
    public void AnimationEventReceived(string eventName);
    public void SubscribeAnimationTriggers();
    public void UnsubscribeAnimationTriggers();
}
