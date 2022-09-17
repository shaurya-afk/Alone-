using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLike : MonoBehaviour
{
    public float speed, dist;
    public Transform groundCheck;
    public bool movingRight = true;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = Random.Range(2, 8);
        anim.SetFloat("Speed", speed);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, dist);
        if (hitInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                movingRight = true;
            }
        }
    }
}
