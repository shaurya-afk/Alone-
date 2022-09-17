using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawner : MonoBehaviour
{
    public GameObject fireBall;
    public float startTime;
    private float timeBtw;
    private float minX, maxX, minY, maxY;
    // Start is called before the first frame update
    void Start()
    {
        timeBtw = startTime;
        minX = -8f;
        maxX = 212f;
        minY = -.9f;
        maxY = 80f;
    }

    // Update is called once per frame
    void Update()
    {
                
        if (timeBtw<=0)
        {
            Instantiate(fireBall, new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY)), Quaternion.identity);
            timeBtw = startTime;
        }
        else
        {
            timeBtw -= Time.deltaTime;
        }
    }
}
