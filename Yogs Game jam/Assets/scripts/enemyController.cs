using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private int direction;

    public bool targetingPlayer = false;
    public bool runningFromPlayer = false;
    public float movementSpeed;

    void Start()
    {
        // set up references
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // start targeting the player if they get too close
        if (Vector3.Distance (player.transform.position,transform.position ) < 3)
        {
            targetingPlayer = true;
        }
        // let the player escape if they get far enough away
        else if (Vector3.Distance(player.transform.position, transform.position) > 5)
        {
            targetingPlayer = false;
        }
    }

    void FixedUpdate()
    {
        if (targetingPlayer)
        {
            // pick closest direction to the player
            if (player.transform.position.x < transform.position.x)
                direction = -1;
            else
                direction = 1;

            // move
            rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
        }
    }
}
