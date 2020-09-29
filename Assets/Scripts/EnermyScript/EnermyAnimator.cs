using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAnimator : MonoBehaviour {

    private Animator anim;
	// Use this for initialization

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Walk(bool walk)
    {
        anim.SetBool(AnimationTags.WALK_PARAMETER, walk);
    }

    public void Run(bool run)
    {
        anim.SetBool(AnimationTags.RUN_PARAMETER, run);
    }

    public void Attack()
    {
        anim.SetTrigger(AnimationTags.ATTACK_TRIGGER);
    }

    public void Dead()
    {
        anim.SetBool(AnimationTags.DEAD_TRIGGER,true);
    }

    public void GetHit()
    {
        anim.SetTrigger(AnimationTags.GETHIT);
    }
}
