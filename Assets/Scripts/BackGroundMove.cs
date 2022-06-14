using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    bool dirX, dirY;
    // Start is called before the first frame update
    void Start()
    {
        dirX = true;
        dirY = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 1.1f && dirX)
        {
            transform.position = new Vector3(transform.position.x + 0.001f, transform.position.y, transform.position.z);
        }
        else
        {
            dirX = false;
            transform.position = new Vector3(transform.position.x - 0.001f, transform.position.y, transform.position.z);
            if (transform.position.x < -1.1f)
            {
                dirX = true;
            }
        }

        if (transform.position.y < 0.9f && dirY)
        {
            transform.position = new Vector3(transform.position.x , transform.position.y + 0.002f, transform.position.z);
        }
        else
        {
            dirY = false;
            transform.position = new Vector3(transform.position.x , transform.position.y - 0.002f, transform.position.z);
            if (transform.position.y < -0.9f)
            {
                dirY = true;
            }
        }
    }
}
