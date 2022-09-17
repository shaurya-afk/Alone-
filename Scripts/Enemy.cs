using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed, dist;
    public Transform groundCheck;
    public bool facingRight = true;

    public GameObject deathParticle;
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, dist);
        if (hitInfo.collider==false)
        {
            if (facingRight)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                facingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingRight = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health -= health;
        }
        if (collision.gameObject.CompareTag("Respawn"))
        {
            health -= health;
        }
    }
}
