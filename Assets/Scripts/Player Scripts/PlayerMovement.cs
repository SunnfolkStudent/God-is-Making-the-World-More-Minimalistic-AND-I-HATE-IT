using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 12f;
    private Vector2 _desiredVelocity;

    [Header("Acceleration")] 
    public float accelerationTime = 0.02f;
    public float groundFriction = 0.03f;
    public float airFriction = 0.005f;

    [Header("CoyoteTime")]
    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;

    [Header("JumpBuffer")]
    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;
    
    [Header("isGrounded")]
    public LayerMask whatIsGround;
    
    [Header("Components")]
    private Rigidbody2D _rigidbody2D;
    private InputManager _input;
    
    public bool canMove = true;
    
    public Animator animator;
    public PlayerHealthManager healthManager;

    public PlayableDirector playableDirector;
    
    private float timer;
    private bool timerDone;
    private bool timerOn;
    
    //private AudioSource _audioSource;
    //public AudioClip[] jumpClips;
    //public AudioClip[] walkClips;

    private Keyboard _keyboard;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input = GetComponent<InputManager>();
        //_audioSource = GetComponent<AudioSource>();
        _keyboard = Keyboard.current;
        animator.SetBool("isRising", true);
    }

    private void Update()
    {
        if (timerOn)
        {
            print("timer on");
            timer += Time.deltaTime;
            if (timer > 1.5f && !timerDone)
            {
                SceneManager.LoadScene("HeavenScene");
            }
        }
        _desiredVelocity = _rigidbody2D.velocity;

        if (!animator.GetBool("isDead") && !animator.GetBool("isRising") && canMove)
        {
            if (_keyboard.dKey.isPressed)
            {
                transform.localScale = new Vector3(-1, 1, 1f);
            }
            else if (_keyboard.aKey.isPressed)
            {
                transform.localScale = new Vector3(1, 1, 1f);
            }
        }

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            //_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
            _desiredVelocity.y = jumpSpeed;
            jumpBufferCounter = 0f;
        }
        
        if (_input.jumpReleased && _desiredVelocity.y > 0f)
        {
            //_audioSource.PlayOneShot(jumpClips[Random.Range(0, jumpClips.Length)]);
            //_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.2f);
            _desiredVelocity.y *= 0.5f;
            coyoteTimeCounter = 0f;
        }


        if (animator.GetBool("isDead") || !canMove || animator.GetBool("isRising"))
        { return; }
        
        if (IsPlayerGrounded())
        { coyoteTimeCounter = coyoteTime; } else { coyoteTimeCounter -= 1 * Time.deltaTime; }

        if (_input.jumpPressed)
        { jumpBufferCounter = jumpBufferTime; } else { jumpBufferCounter -= 1 * Time.deltaTime; }
        
        _rigidbody2D.velocity = _desiredVelocity;
        
        animator.SetFloat("Speed", Mathf.Abs(_desiredVelocity.x));
        animator.SetBool("isJumping", !IsPlayerGrounded());
        animator.SetBool("isFalling", _rigidbody2D.velocity.y < 0);
        animator.SetBool("isDead", healthManager.lives <= 0);
        
    }
    
    private void FixedUpdate()
    {
        if (!canMove)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("isJumping", false);
        }
        
        if (_input.moveDirection.x != 0)
        {
            
            _desiredVelocity.x = Mathf.Lerp(_desiredVelocity.x, moveSpeed * _input.moveDirection.x, accelerationTime);
        }
        else
        {
            
            _desiredVelocity.x = Mathf.Lerp(_desiredVelocity.x, 0f, IsPlayerGrounded() ? groundFriction : airFriction);
        }

        _rigidbody2D.velocity = _desiredVelocity;
    }

    private bool IsPlayerGrounded()
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.red, 1f, true);
        return Physics2D.Raycast(transform.position, Vector2.down, 0.2f, whatIsGround);
    }

    public void setIsRisingToFalse()
    {
        animator.SetBool("isRising", false);
        return;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathBox"))
        {
            print("Die");
            SceneManager.LoadScene("DeathScene");
        }

        if (other.gameObject.CompareTag("HevenBox"))
        {
            playableDirector.Play();
            timerOn = true;
        }
    }

    /*
    public void playWalkSound()
    {
        
        _audioSource.PlayOneShot(walkClips[Random.Range(0, walkClips.Length)]);
        return;
    }
    */
}