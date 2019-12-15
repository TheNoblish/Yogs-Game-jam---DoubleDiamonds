using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    Animator animator;
    private GameObject player;
    public GameObject package;
    private Rigidbody2D rb;
    private int direction;
    private Vector2 runFrom;

    public bool targetingPlayer = false;
    public bool runningFromPlayer = false;
    public bool gotPackage = false;
    public bool targetingPackage = false;
    public bool passive = false;
    public float movementSpeed;
    public float pets;

    void Start()
    {
        // set up references
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        package = GameObject.FindGameObjectWithTag("Package");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        package = GameObject.FindGameObjectWithTag("Package");
        // if petted enough become passive and drop package
        if (pets > 200 && !passive)
        {
            passive = true;
            if (gotPackage)
            {
                //package.SetActive(true);
                package.GetComponent<SpriteRenderer>().enabled = true;
                package.GetComponent<BoxCollider2D>().enabled = true;
                package.transform.position = new Vector3(transform.position.x, transform.position.y+2, transform.position.z);
                package.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            }
        }
        // if the player is carrying the package have the enemy target them
        if (PlayerController.isCarrying && !passive)
        {
            runningFromPlayer = false;
            targetingPackage = false;

            // start targeting the player if they get too close
            if (Vector3.Distance(player.transform.position, transform.position) < 5)
            {
                targetingPlayer = true;
            }
            // let the player escape if they get far enough away
            else if (Vector3.Distance(player.transform.position, transform.position) > 5)
            {
                targetingPlayer = false;
            }
        }
        // run from the player with the package
        else if (gotPackage && !passive)
        {
            targetingPackage = false;
            targetingPlayer = false;
            // run untill far enough away
            if (Vector3.Distance(runFrom, transform.position) < 20)
            {
                runningFromPlayer = true;
            }
            else
            {
                runningFromPlayer = false;
            }
        }
        // else go after the package
        else if (!gotPackage && !passive)
        {
            runningFromPlayer = false;
            targetingPlayer = false;
            // go for the package
            if (Vector3.Distance(package.transform.position, transform.position) < 5)
            {
                targetingPackage = true;
            }
            // stop chasing the package if theyre too far
            else if (Vector3.Distance(package.transform.position, transform.position) > 3)
            {
                targetingPackage = false;
            }
        }

        if (passive)
         pets -= 0.1f;
        if (pets <= 0)
        {
            passive = false;
        }
    }

    void OnCollisionEnter2D (Collision2D other)
    {
       
        // take the package from the ground
       if (other.gameObject.tag == "Package" && !passive)
        {
            gotPackage = true;
            //other.gameObject.SetActive(false);
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            runFrom = transform.position;
        }
        if (other.gameObject.tag == "snowball")
        {;
            runFrom = new Vector3(transform.position.x-21,transform.position.y,0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // grab the package from the player
        if (other.gameObject.tag == "Player" && PlayerController.isCarrying && !passive)
        {
            PlayerController.isCarrying = false;
            gotPackage = true;
            runFrom = transform.position;
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
        else if (targetingPackage)
        {
            // pick closest direction to the package
            if (package.transform.position.x < transform.position.x)
                direction = -1;
            else
                direction = 1;

            // move
            rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
        }
        else if (runningFromPlayer)
        {
            // pick furthers direction to the player
            if (player.transform.position.x > transform.position.x)
                direction = -1;
            else
                direction = 1;

            // move
            rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
        }
        // start running
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        // flip sprites
        Vector3 scale = transform.localScale;
        if (direction == 1)
        {
            scale.x = -1;
            transform.localScale = scale;
        }
        else
        {
            scale.x = 1;
            transform.localScale = scale;
        }
    }
}
