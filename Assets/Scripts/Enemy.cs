using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int currenthealth;
   
    public int damage = 1;
    public bool groundDetected;
    public bool wallDetected;
    private int FacingDirection;
    private Vector2 movement;
    public GameObject enemy;
    private Rigidbody2D rbEnemy;
    // public GameObject deathEffect;

    private void Start()
    {
       
        rbEnemy = enemy.GetComponent<Rigidbody2D>();
        FacingDirection = 1;
    }
   
    [SerializeField]
    private float groundCheckDistance, wallCheckDistance, movementSpeed;

    [SerializeField]
    private Transform groundcheck, wallcheck;

    [SerializeField]
    private LayerMask whatisGround;
    private void Update()
    {
        groundDetected = Physics2D.OverlapCircle(groundcheck.position, groundCheckDistance, whatisGround);
       
        wallDetected = Physics2D.OverlapCircle(wallcheck.position, wallCheckDistance, whatisGround);

        
        //|| wallDetected
        if (!groundDetected || wallDetected)
        {
            //Flip
            Flip();
        }
        else
        {
            //Move
            movement.Set(movementSpeed * FacingDirection, rbEnemy.velocity.y);
            rbEnemy.velocity = movement;
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        enemy.transform.Rotate(0f, 180f, 0f);
    }
   
    public void TakeDamage (int damage)
    {
        health -= 1;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
     //   Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        
        if (collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
        //    Instantiate(ImpactEffect, transform.position, transform.rotation);
       // Destroy(gameObject);
    }

}
