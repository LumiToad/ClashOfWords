using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Action<string> AnimationStart;
    public Action<string> AnimationEnd;
    public Action<string> AnimationEvent;

    Animator animator;
    private GameObject player;
    public GameObject Player
    { set { player = value; } }

    float baseWalkSpeed = 1.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        GetComponentInChildren<StunnedStars>().PlayerAnimations = this;
        SetWalkAnimationSpeed(baseWalkSpeed);
    }

    public void PlayWalking(bool value)
    {
        animator.SetBool("isWalking", value);
    }

    public void SetWalkAnimationSpeed(float value)
    {
        animator.SetFloat("walkSpeed", value);
    }

    public void PlayAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayNutcracker()
    {
        animator.SetTrigger("Cracking");
    }

    public void PlayStunned()
    {
        animator.SetTrigger("Stunned");
    }

    public void OnAnimationStart(string animationName)
    {
        AnimationStart?.Invoke(animationName);
    }

    public void OnAnimationEnd(string animationName)
    {
        Debug.Log(animationName);
        AnimationEnd?.Invoke(animationName);
    }

    public void OnAnimationEvent(string eventName) 
    {
        AnimationEvent?.Invoke(eventName);
    }
}
