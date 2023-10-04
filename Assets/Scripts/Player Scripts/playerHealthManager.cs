using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerHealthManager : MonoBehaviour
{
    
    [Header("Health")]
    public int lives = 3;
    public int maxLives = 3;

    [Header("I-Frames")]
    public bool canTakeDamage;
    public float canTakeDamageTime = 0.2f;
    public float canTakeDamageCounter;

    private AudioSource _audioSource;
    public AudioClip[] hurtClips;
    public AudioClip[] pickupClips;

    public Animator animator;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heart"))
        {
            if (lives >= maxLives) return;
            lives++;
            _audioSource.PlayOneShot(pickupClips[Random.Range(0, pickupClips.Length)]);
            Destroy(other.gameObject);
        }
        return;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (canTakeDamage && (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Projektile") || other.gameObject.CompareTag("Spike")) && lives > 0)
        {
            canTakeDamage = false;
            canTakeDamageCounter = Time.time + canTakeDamageTime;            
            lives--;
            _audioSource.PlayOneShot(hurtClips[Random.Range(0, hurtClips.Length)]);
        }
        
        if (other.gameObject.CompareTag("Projektile"))
        {
                         
            Destroy(other.gameObject);
        }
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (canTakeDamage && (other.CompareTag("Enemy") || other.CompareTag("Projektile") || other.CompareTag("Spike")) && lives > 0)
        {
            
            canTakeDamage = false;
            canTakeDamageCounter = Time.time + canTakeDamageTime;
            lives--;
            animator.SetTrigger("damaged");
            _audioSource.PlayOneShot(hurtClips[Random.Range(0, hurtClips.Length)]);
            
        }
        
        if (other.CompareTag("Projektile"))
        {
            Destroy(other.gameObject);
        }
    }

    public void collideWithEnemy()
    {

        if (canTakeDamage && lives > 0)
        {
            canTakeDamage = false;
            canTakeDamageCounter = Time.time + canTakeDamageTime;
            lives--;
            _audioSource.PlayOneShot(hurtClips[Random.Range(0, hurtClips.Length)]);

        }
    }

    private void Update()
    {
        if (Time.time > canTakeDamageCounter && !canTakeDamage)
        {

            canTakeDamage = true;
        }
    }
}
