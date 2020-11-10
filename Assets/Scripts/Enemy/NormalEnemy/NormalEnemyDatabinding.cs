using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyDatabinding : MonoBehaviour
{
    
    public bool Dead
    {
        set
        {
            if (value)
                anim.SetTrigger(AnimKeyDead);
        }
    }

    public Animator anim;
    private int AnimKeyDead;
    // Use this for initialization
    void Awake()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        //AnimKeySpeed = Animator.StringToHash("Speed");
        //AnimKeyAttack = Animator.StringToHash("Attack");
        AnimKeyDead = Animator.StringToHash("Dead");
    }
}
