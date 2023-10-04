using UnityEngine;
using UnityEngine.Serialization;

public class GodMovment : MonoBehaviour
{
    public float speed = 5f;
    public int health = 35;
    
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    
    private Rigidbody2D _rigidbody2D;

    private int attacks = 2;
    private float attackTime;
    private float attackStopTime = 10;

    private GodEndFightDialog _godEndFightDialog;
    private bool GodIsAlive = true;

    [FormerlySerializedAs("godHasDesended")] public GodStartFightDialog _godStartFight;
    
    //public PlayerHealthManager playerHealthManager;


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _godEndFightDialog = GetComponent<GodEndFightDialog>();
        _godStartFight = GetComponent<GodStartFightDialog>();
    }

    private void FixedUpdate()
    {
        GodIsDead();
       /*if (Random.Range(0, attacks) == 1 && GodIsAlive == true)
       {
            if (attackTime <= attackStopTime)
            {
                _rigidbody2D.velocity = new Vector2(speed * transform.localScale.x, _rigidbody2D.velocity.y);
                attackTime = Time.deltaTime;
            }
             
       }
       else
       {
            _rigidbody2D.gravityScale = 0;
            transform.position = new Vector2(transform.position.x, transform.position.y + 10);
            
            if (!DetectPlayer())
            {
                _rigidbody2D.velocity = new Vector2(speed * transform.localScale.x, _rigidbody2D.velocity.y);
            }
            
            _rigidbody2D.gravityScale = 1;
             
       }*/
        
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projektile") && health > 0)
        {
            health--;
        }
        
        if (other.gameObject.CompareTag("Projektile"))
        {
            Destroy(other.gameObject);
        }
    }

    private bool DetectPlayer()
    {
        return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 3f),
            Vector2.down * transform.localScale, 11f, whatIsPlayer);
    }

    private void GodIsDead()
    {
        if (health !<= 0) return;
        _godEndFightDialog.godIsDead = true;
        GodIsAlive = false;
    }

    private void GodHasDesended()
    {
        _godStartFight.godHasDesended = true;
    }
}
