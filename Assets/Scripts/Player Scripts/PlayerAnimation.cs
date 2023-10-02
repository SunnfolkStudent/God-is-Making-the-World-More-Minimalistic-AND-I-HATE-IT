using UnityEngine;

namespace Player
{
   public class PlayerAnimation : MonoBehaviour
   {
      /*
      private Animator _animator;
      private SpriteRenderer _spriteRenderer;
      private InputManager _input;
      private PlayerMovement _collision;
      private Rigidbody2D _rigidbody2D;

      private void Start()
      {
         _spriteRenderer = GetComponent<SpriteRenderer>();
         _animator = GetComponent<Animator>();
         _input = GetComponent<InputManager>();
         _collision = GetComponent<PlayerMovement>();
         _rigidbody2D = GetComponent<Rigidbody2D>();
      }

      private void Update()
      {
         UpdateAnimation();
      }

      private void UpdateAnimation()
      {
      
         if (_collision.IsPlayerGrounded())
         {
            _animator.Play(_input.moveDirection.x != 0 
               ? "Character_Walk" : "Character_Idle");
         }
         else
         {
            _animator.Play(_rigidbody2D.velocity.y > 0 
               ? "Character_Jump" : "Character_Fall");
         }

         if (_input.moveDirection.x == 0) return;
         var transform1 = transform;
         transform1.localScale = new Vector3(
            _input.moveDirection.x, 1,1);
      }
      */
   }
}