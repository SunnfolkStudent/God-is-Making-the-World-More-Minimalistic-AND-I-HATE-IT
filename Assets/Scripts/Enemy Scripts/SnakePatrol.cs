using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.SceneManagement;

public class SnakePatrol : MonoBehaviour
{
    public float moveSpeed = 4f;
    
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    
    public Transform fallCheckPoint;
    
    private Rigidbody2D _rigidbody2D;
    
    public bool isShooting = false;

    public PlayerHealthManager playerHealthManager;

    private void Start()
    { _rigidbody2D = GetComponent<Rigidbody2D>(); }

    private void FixedUpdate()
    {
        if (!isShooting)
        {
            _rigidbody2D.velocity = new Vector2(moveSpeed * transform.localScale.x, _rigidbody2D.velocity.y);
        }

        return;
    }

    private void LateUpdate()
    {
        if (DetectedWallOrFall())
        {
            transform.localScale = new Vector3(
                -transform.localScale.x, 1f, 1f);
        }

        if (DetectedPlayer())
        {

           // playerHealthManager.collideWithEnemy();
        }

        return;
    }

    private bool DetectedWallOrFall()
    {
        // Raycast -> right (If ground : flip)
        return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right * transform.localScale , 0.6f, whatIsGround)
            || !Physics2D.Raycast(fallCheckPoint.position, Vector2.down, 0.6f, whatIsGround);
    }

    private bool DetectedPlayer()
    {

        return Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(0.5f, 0.5f), 0, Vector2.left, 0.35f, whatIsPlayer) || Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(0.5f, 0.5f), 0, Vector2.right, 0.35f, whatIsPlayer);
    }
}