using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _xMove;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _whatIsGround;

    [Header("Dash")]
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashSpeed;

    private bool isGrounded;

    private Rigidbody2D _rb;
    private Animator _player_Animator;
    private SpriteRenderer _player_SpriteRenderer;

    private bool _isMoving = false;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player_Animator = GetComponentInChildren<Animator>();
        _player_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        X_Movement();

        Jump();

        Animation_Controller();

        Flip_Handler();

        RayCast_GroundCheck();


        _dashTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _dashTime = _dashDuration;
        }

       

    }

    private void RayCast_GroundCheck()
    {
        // Starting ,, kidr jane aa ,, kina distance chida ,, ke detect karna (using LayerMask) .......................  IMP  Simple ......

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _whatIsGround);

        Debug.Log(isGrounded);
    }

    private void Flip_Handler()
    {
        if (_rb.velocity.x > 0)
        {
            _player_SpriteRenderer.flipX = false;
        }
        else if (_rb.velocity.x < 0)
        {
            _player_SpriteRenderer.flipX = true;
        }
    }

    private void Animation_Controller()
    {
        if (_rb.velocity.x > 0 || _rb.velocity.x < 0)
        {
            _isMoving = true;
        }
        else if (_rb.velocity.x == 0)
        {
            _isMoving = false;
        }

        _player_Animator.SetFloat("yVelocity", _rb.velocity.y);

        _player_Animator.SetBool("isMoving", _isMoving);
        _player_Animator.SetBool("isGrounded", isGrounded);
        _player_Animator.SetBool("isDashing", _dashTime > 0);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
    }

    private void X_Movement()
    {
        _xMove = Input.GetAxisRaw("Horizontal");

        if (_dashTime > 0)
        {
            _rb.velocity = new Vector2(_xMove * _dashSpeed, 0);
        }

        else
        {
            _rb.velocity = new Vector2(_xMove * _moveSpeed, _rb.velocity.y); ;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - _groundCheckDistance));
    }
}
