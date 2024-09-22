using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;

    private Vector3 platformLoc;

    // Start is called before the first frame update
    void Start()
    {
        platformLoc = pointB.position; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, platformLoc, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, platformLoc) < 0.1f)
        {
            platformLoc = platformLoc == pointA.position ? pointB.position : pointA.position;
        }
    }
}
