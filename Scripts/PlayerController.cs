using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //player health
    public float health;
    public TextMeshProUGUI healthText;

    //movement
    public float speed = 25f;
    private float moveInput;
    public bool facingRight = true;

    //jump...double,triple jumps
    public float jumpForce = 20f;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = .5f;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extraJumpVal = 2;

    //dash
    public float dashDistance = 15f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode;

    //Player Animation & Particles
    public Animator anim;
    public GameObject finishParticles;

    //player rigidbody 2d reference
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpVal;
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        if (!isDashing)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        else if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
    }
    // Update is called once per frame
    void Update()
    {
        healthText.text = "X " + health.ToString();
        if (health<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (isGrounded == true)
        {
            extraJumps = extraJumpVal;
        }
        if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
            AudioManager.PlayAudio("jump");
        }
        else if (Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            AudioManager.PlayAudio("jump");
        }
        //Dashing Logic
        //dash...left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            {
                //Instantiate(dashParticles, transform.position, Quaternion.identity);
                StartCoroutine(Dash(-1f));
            }
            else
            {
                doubleTapTime = Time.time + .5f;
            }
            lastKeyCode = KeyCode.A;
        }

        //dash...right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
            {
                //Instantiate(dashParticles, transform.position, Quaternion.identity);
                StartCoroutine(Dash(1f));
            }
            else
            {
                doubleTapTime = Time.time + .5f;
            }
            lastKeyCode = KeyCode.D;
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    IEnumerator Dash(float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(.4f);
        isDashing = false;
        rb.gravityScale = 9;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Health"))
        {
            StartCoroutine(Health_Gain());
            AudioManager.PlayAudio("point");
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= .5f;
            AudioManager.PlayAudio("ugh");
            StartCoroutine(Health_Loss());
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(Finish(SceneManager.GetActiveScene().buildIndex + 1));            
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            health -= health;
        }
        if (collision.gameObject.CompareTag("Bomb"))
        {
            health--;
            StartCoroutine(Health_Loss());
        }
        if (collision.gameObject.CompareTag("Restart"))
        {
            StartCoroutine(Restart());
        }
    }
    IEnumerator Finish(int scene_index)
    {
        Instantiate(finishParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(scene_index);
    }
    IEnumerator Health_Gain()
    {
        health++;
        healthText.fontSize = 28;
        healthText.color = Color.green;
        yield return new WaitForSeconds(.5f);
        healthText.color = Color.white;
        healthText.fontSize = 25;
    }
    IEnumerator Health_Loss()
    {
        healthText.fontSize = 20;
        healthText.color = Color.red;
        yield return new WaitForSeconds(.5f);
        healthText.fontSize = 25;
        healthText.color = Color.white;
    }
    IEnumerator Restart()
    {
        Instantiate(finishParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}

