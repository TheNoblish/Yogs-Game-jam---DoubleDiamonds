using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGround : MonoBehaviour
{
    public float length = 45;
    public float startPos;
    public float position;
    public float speed;
    public Rigidbody2D rb;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        position = startPos;
        rb = gameObject.GetComponent<Rigidbody2D>();
 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-1f, 0) * speed;
        position -= speed*Time.deltaTime;
        if (position <= (player.transform.position.x - 75))
        {
            transform.position = new Vector3(player.transform.position.x + 75,transform.position.y,0);
            position = player.transform.position.x + 75;
        }
        else if (position >= (player.transform.position.x + 75))
        {
            transform.position = new Vector3(player.transform.position.x - 75, transform.position.y, 0);
            position = player.transform.position.x - 75;
        }
    }
}
