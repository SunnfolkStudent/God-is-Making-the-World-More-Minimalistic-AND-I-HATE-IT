using System;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class PlayerCubeCombat : MonoBehaviour
{
    private InputCubeManager _inputManager;

    [Header("Attack Variables")]
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [Header("Audio")]
    private AudioSource _audioSource;

    public AudioClip[] slashClip;
    public AudioClip[] snakeExplosionClip;
    
    private void Start()
    {

        _inputManager = GetComponent<InputCubeManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputManager.attackPressed && _inputManager.canMove)
        {
            playSlashSound();
            Attack();
            Debug.Log("attacking");
        }
    }

    private void Attack()
    {
        
        Collider2D[] hitEnemies =Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hitEnemy");
            Destroy(enemy.gameObject);
            _audioSource.PlayOneShot(snakeExplosionClip[Random.Range(0, snakeExplosionClip.Length)]);
        }
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
