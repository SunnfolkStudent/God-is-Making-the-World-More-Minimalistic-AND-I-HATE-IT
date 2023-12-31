using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerCubeMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 12f;
    private Vector2 _desiredVelocity;

    [Header("Acceleration")] 
    public float accelerationTime = 0.02f;
    public float groundFriction = 0.03f;
    public float airFriction = 0.005f;
    
    [Header("JumpBuffer")]
    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;
    
    [Header("isGrounded")]
    public LayerMask whatIsGround;
    
    [Header("Components")]
    private Rigidbody2D _rigidbody2D;
    private InputCubeManager _input;
    
    public bool canMove = true;
    
    private PlayerHealthManager healthManager;
    
    private PlayerShoot _playerShoot;
    
    private SceneController _sceneController;
    
    //private AudioSource _audioSource;
    //public AudioClip[] jumpClips;
    //public AudioClip[] walkClips;

    private Keyboard _keyboard;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input = GetComponent<InputCubeManager>();
        healthManager = GetComponent<PlayerHealthManager>();
        //_audioSource = GetComponent<AudioSource>();
        _keyboard = Keyboard.current;
        _playerShoot = GetComponent<PlayerShoot>();
        _sceneController = GetComponent<SceneController>();
    }

    private void Update()
    {
        
        _desiredVelocity = _rigidbody2D.velocity;

        if (canMove)
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

        if (healthManager.lives == 0) { transform.localScale = new Vector3(1, 0.5f, 0.4f); }
        
        if (jumpBufferCounter > 0 && IsPlayerGrounded() && canMove)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpSpeed);
            _desiredVelocity.y = jumpSpeed;
            jumpBufferCounter = 0f;
        }
        
        if (_input.jumpReleased && _desiredVelocity.y > 0f)
        {
            //_audioSource.PlayOneShot(jumpClips[Random.Range(0, jumpClips.Length)]);
            //_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.2f);
            _desiredVelocity.y *= 0.5f;
        }

        if (healthManager.lives == 0)
        {
            canMove = false;
            _input.canMove = false;
            _playerShoot.canAttack = false;
        }

        if (_input.jumpPressed && canMove)
        { jumpBufferCounter = jumpBufferTime; } else { jumpBufferCounter -= 1 * Time.deltaTime; }
        
        _rigidbody2D.velocity = _desiredVelocity;
        
    }
    
    private void FixedUpdate()
    {
        if (!canMove) return;
        
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
        return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 1f, whatIsGround);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathBox"))
        {
            print("Die");
            _sceneController.LoadSceneByName("DeathScene");
        }
    }
}