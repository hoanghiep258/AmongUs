using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Databinding : MonoBehaviour
{
    public bool Attack
    {
        set
        {
            if (value)
                animator.SetTrigger(AnimKeyAttack);
        }
    }
    public bool Dead
    {
        set
        {
            if (value)
                animator.SetTrigger(AnimKeyDead);
        }
    }
    public float Speed
    {
        set
        {
            animator.SetFloat(AnimKeySpeed, value);
        }
    }
    public Animator animator;
    private int AnimKeySpeed;
    private int AnimKeyDead;
    private int AnimKeyAttack;
    // Use this for initialization
    void Awake()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        AnimKeySpeed = Animator.StringToHash("Speed");
        AnimKeyAttack = Animator.StringToHash("Attack");
        AnimKeyDead = Animator.StringToHash("Dead");
    }
}
