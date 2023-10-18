using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class PlayerCombat : MonoBehaviour
{
    private InputManager _inputManager;
    public ParticleSystem particles;

    [Header("Attack Variables")]
    public Transform AttackPoint;
    public float attackRange = 0.7f;
    public LayerMask enemyLayers;

    private Animator animator;

    [Header("Audio")]
    private AudioSource _audioSource;

    public AudioClip[] slashClip;
    public AudioClip[] snakeExplosionClip;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        _inputManager = GetComponent<InputManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputManager.attackPressed && !animator.GetBool("isRising"))
        {
            animator.SetBool("isAttacking", true);
            
        }
    }

    private void Attack()
    {
        
        Collider2D[] hitEnemies =Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hitEnemy");
            Instantiate(particles, new Vector2(enemy.transform.position.x, enemy.transform.position.y + 0.5f), quaternion.identity);
            Destroy(enemy.gameObject);
            _audioSource.PlayOneShot(snakeExplosionClip[Random.Range(0, snakeExplosionClip.Length)]);
        }
        animator.SetBool("isAttacking", false);
        
    }

    private void playSlashSound()
    {
        
        _audioSource.PlayOneShot(slashClip[Random.Range(0, slashClip.Length)]);
        return;
    }
    
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
