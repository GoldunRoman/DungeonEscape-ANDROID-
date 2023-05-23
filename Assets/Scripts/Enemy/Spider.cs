using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }

    [SerializeField]
    private GameObject _acidPrefab;

    //Use for initialize
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Update()
    {
        
    }

    public override void IdleMovement()
    {
        //Sit still
    }

    public void Attack()
    {
        if(isDead)
            return;


        //Debug.Log("Spider::Attack()");
        Instantiate(_acidPrefab, this.transform);
    }

    public void Dammage()
    {
        if (isDead) return;

        Debug.Log("Spider::Dammage()");
        Health--;
        if (Health < 1)
        {
            isDead = true;
            GetComponent<BoxCollider2D>().enabled = false;
            animator.SetTrigger("Death");

            player.AddDiamonds(5);
        }
            

    }
}
