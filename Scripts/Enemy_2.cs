using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    //enemy movement
    public float speed;
    public float dist;
    public Transform groundCheck;
    public bool movingRight = true;

    //enemy shooting
    private float timeBtwShot;
    public float startTimeBtwShot;
    public GameObject projectilePrefab;
    public Transform bombSpawner;
    public float minDis;
    public Transform target;

    //enemy health
    public float health;
    public GameObject deathParticle;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwShot = startTimeBtwShot;
        health = 2f;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        minDis = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health<=0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, dist);
        if (hitInfo.collider==false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        if (timeBtwShot<=0 && Vector2.Distance(transform.position,target.position)<=minDis)
        {
            Instantiate(projectilePrefab, bombSpawner.position, Quaternion.identity);
            timeBtwShot = startTimeBtwShot;
        }
        else
        {
            timeBtwShot -= Time.deltaTime;
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
