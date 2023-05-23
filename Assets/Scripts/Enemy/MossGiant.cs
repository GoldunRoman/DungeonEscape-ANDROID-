using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }


    //Use for initialize
    public override void Init()
    {
        base.Init();
        Health = base.health;
        
    }

    public override void IdleMovement()
    {
        base.IdleMovement();        
    }

    public override void InCombatMovement()
    {
        base.InCombatMovement();
    }

    public void Dammage()
    {
        if (isDead) return;

        Debug.Log("MossGiant::Dammage()");
        //hit animation
        animator.SetTrigger("Hit");

        //health system
        Health--;
        if(Health < 3)
        {
            animator.Play("Attack", 0);
        }

        if (Health < 1)
        {
            isDead = true;
            GetComponent<BoxCollider2D>().enabled = false;
            animator.SetTrigger("Death");

            player.AddDiamonds(10);
        }
            
    }
}
