using System;
using UnityEngine;

public class Projektile : MonoBehaviour
{
    public float speed = 20f;
    public float arc = 0.5f;

    public Rigidbody2D _rigidbody2D;

    public float shootDirection;

    private void Start()
    {
        _rigidbody2D.velocity = new Vector2(transform.position.x * speed*shootDirection,transform.position.x * arc);
    }
    
}
