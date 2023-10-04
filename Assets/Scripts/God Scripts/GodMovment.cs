using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class GodMovment : MonoBehaviour
{
    public float speed = 5f;
    public int health = 35;
    
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
    
    private Rigidbody2D _rigidbody2D;

    public int attacks = 2;
    public float attackTime;
    public float attackStopTime = 10;

    public GodEndFightDialog _godEndFightDialog;
    public bool GodIsAlive = false;
    public bool canChekIfGodIsDead = false;
    public bool flyingAttack;

    public TimelineClip _clip;
    public PlayableDirector _dir;

    
    //public PlayerHealthManager playerHealthManager;


    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //_godEndFightDialog = GetComponent<GodEndFightDialog>();
        //_godStartFight = GetComponent<GodStartFightDialog>();
    }

    private void FixedUpdate()
    {
        if (canChekIfGodIsDead)
        {
           GodIsDead(); 
        }
        
        if (!DetectPlayer() && flyingAttack)
        {
            _rigidbody2D.velocity = new Vector2(speed * transform.localScale.x, _rigidbody2D.velocity.y);
        }
        if (DetectPlayer())
        {
            flyingAttack = false;
            attackTime = 0;
        }

        print(Time.time);
        if (Time.time < attackTime) return;
        print("This is running?");
        
        _rigidbody2D.gravityScale = 1;
        flyingAttack = false;
        
        attacks = Random.Range(0, 2);
        print("Attacks: "+attacks);
        
       if (attacks == 1 && GodIsAlive == true)
       { 
           _rigidbody2D.velocity = new Vector2(speed * transform.localScale.x, _rigidbody2D.velocity.y);
       }
       else
       {
            _rigidbody2D.gravityScale = 0;
            transform.position = new Vector2(transform.position.x, transform.position.y + 10);
            flyingAttack = true;
       }

       attackTime = Time.time + attackStopTime;
    }

    private void LateUpdate()
    {
        if (DetectedWall())
        {
            transform.localScale = new Vector3(-transform.localScale.x, 5f, 1f);
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
        if (health > 0 || !GodIsAlive) 
            return;
        
        GodIsAlive = false;
        
        _godEndFightDialog.godIsDead = true; print("What now?");
    }
}
