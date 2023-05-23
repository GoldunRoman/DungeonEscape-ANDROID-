using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageable
{
    [Header("Main Settings")]
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private float _movementSpeed = 60f;
    [SerializeField]
    private float _jumpForce = 10f;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private int _diamondsAmount = 0;

    [Header("Animations")]
    [SerializeField]
    private PlayerAnimationsController _animController;

    [Header("Child Sprites")]
    [SerializeField]
    private SpriteRenderer _swordArcSprite;
    [SerializeField]
    private SpriteRenderer _playerSprite;

    private bool _resetJump;
    private bool _isGrounded = false;
    private float _horizontalInput;

    public int Health { get; set; }
    public bool isDead = false;

    private void Start()
    {
        Health = 4;
        _rb = GetComponent<Rigidbody2D>();
        _animController = GetComponent<PlayerAnimationsController>();
        if(_animController == null)
        {
            Debug.Log("PAC is NULL!");
        }

        _playerSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (_playerSprite == null)
            Debug.Log("PlayerSprite is NULL!");

        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (_swordArcSprite == null)
            Debug.Log("SwordArcSprite is NULL!");

    }

    private void Update()
    {
        Jump();
        Movement();
        AttackDefault();
    }



    private void Movement()
    {
        if (Health < 1) 
        {
            _movementSpeed = 0f;
            return;
        } 

        _horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal"); 
        _isGrounded = IsGrounded();
        _rb.velocity = new Vector2(_horizontalInput * _movementSpeed, _rb.velocity.y); 
        _animController.MoveAnimation(_horizontalInput);
    }

    private void Jump()
    {
        if (Health < 1) return;

        if (CrossPlatformInputManager.GetButtonDown("B_Button"))
        {                    
            if (IsGrounded())
            {                
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                StartCoroutine(ResetJumpRoutine());
                _animController.JumpAnimation(true);
            }          
        }        
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, _groundLayer);
        Debug.DrawRay(position, direction, Color.yellow);
        if(hit.collider != null)
        {
            if (_resetJump == false)
            {
                _animController.JumpAnimation(false);
                return true;
            }
        }

        return false;
    }

    private void AttackDefault()
    {
        if (Health < 1) return;

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && _isGrounded == true)
        {
            _swordArcSprite.flipX = _playerSprite.flipX;

            Transform swordArcTransform = transform.GetChild(1);
            if (_swordArcSprite.flipX == true)
            {
                swordArcTransform.localPosition = new Vector2(-0.25f, -0.11f);
                swordArcTransform.localRotation = Quaternion.Euler(-66f, 48f, 80f);
            }
            else
            {
                swordArcTransform.localPosition = new Vector2(0.25f, -0.11f);
                swordArcTransform.localRotation = Quaternion.Euler(66f, 48f, -80f);
            }
            _animController.AttackAnimation(_horizontalInput);
            
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Dammage()
    {
        if (isDead == true)
        {          
            return;
        }

        Debug.Log("Player::Dammage()");

        Health--;
        UIManager.Instance.UpdateHealth(Health);

        if (Health < 1)
        {
            isDead = true;
            _animController.DeathAnimation();
        }
    }

    public void AddDiamonds(int gems = 1)
    {
        _diamondsAmount += gems;
        UIManager.Instance.UpdateGemsCount(_diamondsAmount);
    }

    public void TakeDiamonds(int gems)
    {
        _diamondsAmount -= gems;
        UIManager.Instance.UpdateGemsCount(_diamondsAmount);
    }

    public int GetDiamonds()
    {
        return _diamondsAmount;
    }

}
