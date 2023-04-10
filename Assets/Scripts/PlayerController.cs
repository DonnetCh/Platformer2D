using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public int health = 3;
    public float moveSpeed = 2f;
    public float jumpForce = 50f;
    public bool isJumping;
    public bool facingRight = true;
    private float gravity;
    float moveHorizontal;
    float moveVertical;
    public TextMeshProUGUI vida;

   // public int defaultLives;
    public int defaultScore;
    public int HighScore;
    // Start is called before the first frame update
    void Start()
    {
        health = PlayerPrefs.GetInt("CurrentLives");
        isJumping = false;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        vida.text = ("x") + health.ToString();
        if (transform.position.y <= -7)
        {
            health = health - 1;
            PlayerPrefs.SetInt("CurrentLives", health);
            SceneManager.LoadScene(0);
            Debug.Log("ReinicioEscenaBAjo");
        }

        if (health <= -0.1)
        {
            SceneManager.LoadScene(2);
        }
        Debug.Log(health);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        PlayerPrefs.SetInt("CurrentLives", health);
        if (health <= 0)
        {
            Die();
            Debug.Log("Tiene0devida");
        }
    }
    public void Die()
    {
          Destroy(gameObject);
        SceneManager.LoadScene(0);
        Debug.Log("ReinicioEscenaMurio");
    }
    void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
           
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed,0f), ForceMode2D.Impulse);
            if (moveHorizontal > 0.1f && facingRight)
            {
                Flip();
            }
            else if (moveHorizontal < -0.1f && !facingRight)
            {
                Flip();
            }
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }

        if (collision.gameObject.tag == "Puerta")
        {
            PlayerPrefs.SetInt("CurrentLives", health);
            SceneManager.LoadScene(3);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = true;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

   
}
