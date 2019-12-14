using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwStuff : MonoBehaviour
{
    public GameObject snowball;
    public float minThrowForce;
    public float maxThrowForce;
    public float throwSpeed;
    public float throwForce;
    public Camera cam;
    private bool thrown;

    // Start is called before the first frame update
    void Start()
    {
        throwForce = minThrowForce;
    }

    // Update is called once per frame
    void Update()
    {
        // hold down to charge throw
        if (throwForce >= maxThrowForce && !thrown)
        {
            throwForce = maxThrowForce;
            throwBall();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            thrown = false;
            throwForce = minThrowForce;
        }
        else if (Input.GetButton("Fire2") && !thrown)
        {
            throwForce += throwSpeed * Time.deltaTime;
        }
        else if (Input.GetButtonUp("Fire2") && !thrown)
        {
            throwBall();
        }
    }


    void throwBall()
    {
        thrown = true;

        GameObject ball = Instantiate(snowball, transform.position, transform.rotation) as GameObject;

        //ball.GetComponent<Rigidbody2D>().velocity = new Vector3(1*throwForce,0f,0f);
        ball.GetComponent<Rigidbody2D>().velocity = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position) * throwForce;
        throwForce = minThrowForce;
    }
}
