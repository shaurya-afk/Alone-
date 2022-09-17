using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Respawn") || collision.gameObject.CompareTag("Finish") || collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            AudioManager.PlayAudio("bomb");
            Destroy(gameObject);
        }
    }
}
