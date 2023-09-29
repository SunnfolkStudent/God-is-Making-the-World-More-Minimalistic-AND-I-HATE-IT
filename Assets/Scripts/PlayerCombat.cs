using System;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerCombat : MonoBehaviour
{
    private InputManager _inputManager;

    public Transform AttackPoint;
    public float attackRange = 0.5;

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputManager.attackPressed)
        {
            Attack();
        }
    }

    private void Attack()
    {
        
    }
}
