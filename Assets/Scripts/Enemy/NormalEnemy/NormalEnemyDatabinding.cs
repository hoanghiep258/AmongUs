using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyDatabinding : MonoBehaviour
{

    public float Speed
    {
        set
        {
            anim.SetFloat(AnimKeySpeed, value);
        }
    }

    public bool Attack
    {
        set
        {
            if (value)
                anim.SetTrigger(AnimKeyAttack);
        }
    }

    public bool Dead
    {
        set
        {
            if (value)
                anim.SetTrigger(AnimKeyDead);
        }
    }

    private Animator anim;
    private int AnimKeySpeed;
    private int AnimKeyAttack;
    private int AnimKeyDead;
    // Use this for initialization
    void Awake()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        AnimKeySpeed = Animator.StringToHash("Speed");
        AnimKeyAttack = Animator.StringToHash("Attack");
        AnimKeyDead = Animator.StringToHash("Dead");
    }
}
