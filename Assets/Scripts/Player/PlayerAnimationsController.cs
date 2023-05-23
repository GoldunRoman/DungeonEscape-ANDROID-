using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{
    [SerializeField]
    private Animator _spriteAnimator;
    [SerializeField]
    private Animator _swordArcAnimator;
    [SerializeField]
    private SpriteRenderer _playerSprite;
    [SerializeField]
    private SpriteRenderer _swordArcSprite;
    [SerializeField]
    private Transform _swordArcTransform;

    private void Start()
    {
        _spriteAnimator = GetComponentInChildren<Animator>();
        if (_spriteAnimator == null)
            Debug.Log("Sprite Animator is NULL!");
                

        _swordArcAnimator = transform.GetChild(1).GetComponent<Animator>();
        if (_swordArcAnimator == null)
            Debug.Log("SwordArc Animator is NULL!");


        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        if (_playerSprite == null)
            Debug.Log("Player Sprite handle is NULL!");

        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (_swordArcSprite == null)
            Debug.Log("SwordArcSprite is NULL!");

        _swordArcTransform = transform.GetChild(1).GetComponent<Transform>();
        if (_swordArcTransform == null)
            Debug.Log("SwordArcTransform is NULL!");
    }

    public void MoveAnimation(float moveSpeed)
    {
        Flip(moveSpeed, _playerSprite);

        _spriteAnimator.SetFloat("Move", Mathf.Abs(moveSpeed));
    }

    public void JumpAnimation(bool isJumping)
    {
        _spriteAnimator.SetBool("Jumping", isJumping);
    }

    public void AttackAnimation(float moveSpeed)
    {
        _spriteAnimator.SetTrigger("Attack");
        if (_spriteAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            _swordArcAnimator.SetTrigger("SwordAnimation");
    }

    private void Flip(float moveSpeed, SpriteRenderer sprite)
    {
        if (moveSpeed < 0)
            sprite.flipX = true;
        else if (moveSpeed > 0)
            sprite.flipX = false;
    }

    public void DeathAnimation()
    {
        _spriteAnimator.SetTrigger("Death");
    }

}
