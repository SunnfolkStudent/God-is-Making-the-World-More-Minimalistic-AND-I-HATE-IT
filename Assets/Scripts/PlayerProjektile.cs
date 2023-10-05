using System;
using UnityEngine;

public class PlayerProjektile : MonoBehaviour
{
    public float speed = 20f;
    public float arc = 0.5f;

    public Rigidbody2D rigidbody2D;

    private void Start()
    {
        
        rigidbody2D.velocity = new Vector2(speed,arc);
    }
    
}
