using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health, currentSpeed, gems, defaultSpeed;
    [SerializeField]
    protected Transform pointA, pointB; 
    [SerializeField]
    protected Player player;
    [SerializeField]
    protected GameObject diamondPrefab;

    protected Vector3 direction;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    //protected bool isHit = false;
    protected bool isCombatMode = false, isIdleMode = false, isDead = false;

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        direction = pointA.position;
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        IdleMovement();
        InCombatMovement();
    }
    
    public virtual void IdleMovement()
    {
        //Disable movement logic if dead or in CM
        if(isDead || isCombatMode)
            return;

        animator.SetBool("inCombat", false);
        //Turns the enemy in the direction of movement
        if (direction == pointA.position)
        {
            spriteRenderer.flipX = true;
        }
        else if(direction == pointB.position)
        {
            spriteRenderer.flipX = false;
        }

        //Normal movement (non-combat mode)
        
        transform.position = Vector3.MoveTowards(transform.position, direction, currentSpeed * Time.deltaTime);        

        //Stops if idle animation
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && animator.GetBool("inCombat") == false)
            currentSpeed = 0;
        else
            currentSpeed = defaultSpeed;
        
    }

    public virtual void InCombatMovement()
    {
        if (isDead || !isCombatMode)
            return;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && animator.GetBool("inCombat") == true)
            animator.Play("Walk", 0);


        //Go to player pos
        Vector3 playerPosition = player.transform.localPosition - transform.localPosition;

        //Flip sprite if Player on the left side
        if (playerPosition.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerPosition.x > 0)
        {
            spriteRenderer.flipX = false;
        }


        //When Player near Enemy -> start combat animation 
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), currentSpeed * Time.deltaTime); 

        if(distance < 1.2f)
        {
            currentSpeed = 0;
            animator.SetBool("inCombat", true);
        }
        else
        {
            currentSpeed = defaultSpeed;
            animator.SetBool("inCombat", false);
        }
    }

    public void SwitchCombatMode(bool switcher)
    {
        if (switcher == true)
            isCombatMode = true;
        else
            isCombatMode = false;
    }

    public virtual void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Left"))
        {
            animator.SetTrigger("Idle");
            direction = pointB.position;
        }
        else if (trigger.gameObject.CompareTag("Right"))
        {
            animator.SetTrigger("Idle");
            direction = pointA.position;
        }
    }

}
