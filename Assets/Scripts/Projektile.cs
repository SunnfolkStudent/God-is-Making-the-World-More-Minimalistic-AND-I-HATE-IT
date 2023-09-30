using System;
using UnityEngine;

public class Projektile : MonoBehaviour
{
    public float speed = 20f;
    public float arc = 0.5f;

    public Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D.velocity = new Vector2(transform.position.x * speed,transform.position.x * arc);
    }
    
}
