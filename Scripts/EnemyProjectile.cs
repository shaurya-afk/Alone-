using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    Transform target;
    private float health = 1f;
    public GameObject explosionParticles;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            AudioManager.PlayAudio("bomb");
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        StartCoroutine(Bomb());
    }
    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(2.5f);
        health -= health;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health -= health;
        }
    }
}
