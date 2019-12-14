using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGround : MonoBehaviour
{
    public float length = 45;
    public float startPos;

    public GameObject camera;
    public float ParallaxEffext;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        float temp = camera.transform.position.x * (1 - ParallaxEffext);
        float distance = camera.transform.position.x * ParallaxEffext;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
