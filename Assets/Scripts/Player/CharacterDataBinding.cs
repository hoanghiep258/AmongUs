using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataBinding : MonoBehaviour {

    public Animator animator;
    public Animator handAnimator;
    private int AnimKeySpeed;
    private int AnimKeyAttack;
    private int AnimKeyDead;
    public float Speed{
        set{
            animator.SetFloat(AnimKeySpeed, value);
        }
    }
    public int Attack{
        set{            
                handAnimator.SetInteger(AnimKeyAttack, value);
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
	// Use this for initialization
	private void Awake()
	{
        AnimKeySpeed = Animator.StringToHash("Speed");
        AnimKeyAttack = Animator.StringToHash("Attack");
        AnimKeyDead = Animator.StringToHash("Dead");
	}
    public void Setup(AnimatorOverrideController animatorOverrideController)
    {
        
        //animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

    }
}
