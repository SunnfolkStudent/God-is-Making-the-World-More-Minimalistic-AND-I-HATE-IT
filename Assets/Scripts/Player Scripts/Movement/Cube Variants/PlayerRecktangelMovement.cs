using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class PlayerRecktangelMovement : MonoBehaviour
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
    
    public PlayableDirector playableDirector;
    
    private float timer;
    private bool timerDone;
    private bool timerOn;
    private int wichTimer = 0;
    
    
    private PlayerHealthManager healthManager;
    
    //private AudioSource _audioSource;
    //public AudioClip[] jumpClips;
    //public AudioClip[] walkClips;

    private Keyboard _keyboard;

    private SceneController _sceneController;
    
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _input = GetComponent<InputCubeManager>();
        healthManager = GetComponent<PlayerHealthManager>();
        //_audioSource = GetComponent<AudioSource>();
        _keyboard = Keyboard.current;
        _sceneController = GetComponent<SceneController>();
    }

    private void Update()
    {
        if (timerOn)
        {

            timer += Time.deltaTime;
            if (timer > 1.5f && !timerDone)
            {
                _sceneController.LoadSceneByName("HeavenSceneThree");
            }
        }

        _desiredVelocity = _rigidbody2D.velocity;

        
        if (_keyboard.dKey.isPressed)
        {
            transform.localScale = new Vector3(-1, 1, 1f);
        }
        else if (_keyboard.aKey.isPressed)
        { 
            transform.localScale = new Vector3(1, 1, 1f);
        }
        

        if (jumpBufferCounter > 0 && IsPlayerGrounded())
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

        

        if (_input.jumpPressed)
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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathBox"))
        {
            print("Die");
            _sceneController.LoadSceneByName("DeathScene");
        }
        
        if (other.gameObject.CompareTag("HevenWarpBoxThree"))
        {
            playableDirector.Play();
            timerOn = true;
        }
    }
    
    private bool IsPlayerGrounded()
    {
        return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 0.2f, whatIsGround);
    }
    
    
    /*
    public void playWalkSound()
    {
        
        _audioSource.PlayOneShot(walkClips[Random.Range(0, walkClips.Length)]);
        return;
    }*/
    
}