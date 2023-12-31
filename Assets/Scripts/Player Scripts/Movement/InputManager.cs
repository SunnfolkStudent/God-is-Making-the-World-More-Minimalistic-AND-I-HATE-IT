using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Movement")]
// This variable is used to hold the Input value from WASD, Dpad or Left Stick
    [HideInInspector] public Vector2 moveDirection;

    [Header("Jump")]
    
    [HideInInspector] public bool canJump;
// These variables are used to hold Input values from Spacebar or South button
    [HideInInspector] public bool jumpPressed, jumpReleased, jumpHeld;

    [Header("Interact")]
    [HideInInspector] public bool canInteract;
// These variables are used to hold Input Values from the F key or East Button
    [HideInInspector] public bool interactPressed, interactReleased, interactHeld;

    [Header("attack")] 
    [HideInInspector] public bool canAttack;

    [HideInInspector] public bool attackPressed;

    public bool canMove = true;
    
// These variables are used to determine input source.
    [SerializeField] private bool usingGamepad, usingDpad;

// These variables are used to hold the current Input source
    
    private Keyboard _keyboard;
    private Gamepad _gamepad;

    private Animator animator;
    
    
    private void Start()
    {
    //Assign Input Sources to Variables
        _keyboard = Keyboard.current;
        _gamepad = Gamepad.current;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
    // Check whether we are using Gamepad or Keyboard
        if (usingGamepad && _gamepad != null)
        {
            UpdateGamepadInput();  
        }
        else
        {
            UpdateKeyboardInput();
        }
    }
    
    private void UpdateKeyboardInput()
    {
        
    // Set the value of moveDirection to be equal to the value of wasd
        moveDirection.x = (_keyboard.dKey.isPressed ? 1 : 0) + (_keyboard.aKey.isPressed ? -1 : 0);
        moveDirection.y = (_keyboard.wKey.isPressed ? 1 : 0) + (_keyboard.sKey.isPressed ? -1 : 0);
    // Set the jump bools when spacebar is interacted with
        jumpPressed = _keyboard.spaceKey.wasPressedThisFrame;
        jumpReleased = _keyboard.spaceKey.wasReleasedThisFrame;
        jumpHeld = _keyboard.spaceKey.isPressed; 
    // Set the interact bools when the f key is interacted with
        interactPressed = _keyboard.eKey.wasPressedThisFrame;
        interactReleased = _keyboard.eKey.wasReleasedThisFrame;
        interactHeld = _keyboard.eKey.isPressed;
    // Set the attack bools wen the g key is interacted with
        attackPressed = _keyboard.wKey.wasPressedThisFrame;

    if (!canMove)
    {
        moveDirection.x = 0;
    }
    
    if (animator.GetBool("isDead") || animator.GetBool("isRising"))
    {
        animator.SetBool("isJumping", false);
        moveDirection.x = 0;
        //moveDirection.y = 0;
    }
    
    }
    
    private void UpdateGamepadInput()
    {
        if (usingDpad)
        {
            moveDirection.x = (_gamepad.dpad.right.isPressed ? 1 : 0) + (_gamepad.dpad.left.isPressed ? -1 : 0);
            moveDirection.y = (_gamepad.dpad.up.isPressed ? 1 : 0) + (_gamepad.dpad.down.isPressed ? -1 : 0); 
        }
        else
        {
            moveDirection = _gamepad.leftStick.ReadValue();
        }
    
        jumpPressed = _gamepad.buttonSouth.wasPressedThisFrame;
        jumpReleased = _gamepad.buttonSouth.wasReleasedThisFrame;
        jumpHeld = _gamepad.buttonSouth.isPressed;
        
        interactPressed = _gamepad.buttonEast.wasPressedThisFrame;
        interactReleased = _gamepad.buttonEast.wasReleasedThisFrame;
        interactHeld = _gamepad.buttonEast.isPressed;
    }

  
}