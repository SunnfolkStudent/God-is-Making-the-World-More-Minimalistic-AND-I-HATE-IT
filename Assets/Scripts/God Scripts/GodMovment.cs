using System;
using UnityEngine;

public class GodMovmen : MonoBehaviour
{
    public float speed = 5f;
    public int health = 35;
    
    public LayerMask whatIsGround;
    //public LayerMask whatIsPlayer;
    
    private Rigidbody2D _rigidbody2D;
    
    //public PlayerHealthManager playerHealthManager;


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        _rigidbody2D.velocity = new Vector2(speed * transform.localScale.x, _rigidbody2D.velocity.y);
        
    }

    private void LateUpdate()
    {
        if (DetectedWall())
        {
            transform.localScale = new Vector3(
                -transform.localScale.x, 5f, 1f);
        }
    }

    private bool DetectedWall()
    {
        // Raycast -> right (If ground : flip)
        return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 3f),
            Vector2.right * transform.localScale, 3.5f, whatIsGround);
    }
}
