using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Walk(float speed)
    {
        anim.SetFloat(TagManager.Tags.Walk, Mathf.Abs(speed));
    }

    public void Jump(bool jump)
    {
        anim.SetBool(TagManager.Tags.Jumping_Bool, jump);
    }

    public void Punch()
    {
        anim.SetTrigger(TagManager.Tags.PunchTrigger);
    }

    public void Punch2()
    {
        anim.SetTrigger(TagManager.Tags.Punch2Trigger);
    }

    public void Kick()
    {
        anim.SetTrigger(TagManager.Tags.KickTrigger);
    }

    public void Hurt()
    {
        anim.SetTrigger(TagManager.Tags.HurtAttackTag);
    }

    public void Defense(bool defense)
    {
        anim.SetBool(TagManager.Tags.DefenseBool, defense);
    }

    public void Die(bool die)
    {
        anim.SetBool(TagManager.Tags.DieBool, die);
    }

    
}
