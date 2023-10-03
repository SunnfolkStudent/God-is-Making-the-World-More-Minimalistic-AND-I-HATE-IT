using System;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerCombat : MonoBehaviour
{
    private InputManager _inputManager;

    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public Animator animator;
    
    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputManager.attackPressed)
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
            Destroy(enemy.gameObject);
        }
        animator.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
}
