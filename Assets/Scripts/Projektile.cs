using System;
using Unity.Mathematics;
using UnityEngine;

public class Projektile : MonoBehaviour
{
    public ParticleSystem particles;
    
    public float speed = 20f;
    public float arc = 0.5f;

    public Rigidbody2D rigidbody2D;

    public float shootDirection;

    private float timer;
    private bool timerDone;
    private bool timerOn = false;
    private void Start()
    {
        
        rigidbody2D.velocity = new Vector2(shootDirection * speed,arc);
    }

    private void OnDestroy()
    {
        Instantiate(particles, transform.position, quaternion.identity);
    }
}
