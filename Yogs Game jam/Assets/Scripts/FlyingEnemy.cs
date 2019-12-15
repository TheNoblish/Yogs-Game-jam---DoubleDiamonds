using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    Animator animator;

    GameObject player;
    public GameObject package;
    public bool returned = true;
    public bool delivery;
    Rigidbody2D rigidbody2D;
    int patrolDirection = -1;

    Vector2 playerPosition;

    public bool isAttacking;
    public bool isGrabbing;
    public bool gotPackage;
    public float speed;
    public float attackRange;
    public Transform nest;
    //public nest nestScript;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        package = GameObject.Find("package");
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && !gotPackage && !isGrabbing)
        {
            animator.SetBool("isDiving", false);
            rigidbody2D.velocity = new Vector2(1.5f*patrolDirection, 0) * speed;
        } else if (isAttacking)
        {

            animator.SetBool("isDiving", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (player.transform.position.x > transform.position.x)
            {
                rigidbody2D.velocity = new Vector2(1.5f * 1f, rigidbody2D.velocity.y);
            }
            else
                rigidbody2D.velocity = new Vector2(1.5f * -1f, rigidbody2D.velocity.y);
        }
        else if (isGrabbing)
        {

            animator.SetBool("isDiving", true);
            transform.position = Vector2.MoveTowards(transform.position, package.transform.position, speed * Time.deltaTime);
        }
        else if (gotPackage)
        {
            animator.SetBool("isDiving", false);
            
                transform.position = Vector2.MoveTowards(transform.position, nest.position, speed * Time.deltaTime);
            
        }

        if (Mathf.Abs(nest.position.x - transform.position.x) > 40 && returned)
        {
            patrolDirection = -1;
            returned = false;
            delivery = false;
        }
        else if (Mathf.Abs(nest.position.x - transform.position.x) < 5)
            patrolDirection = 1;
            returned = true;

        if (isAttacking)
            gameObject.layer = 11;
        else
            gameObject.layer = 10;
        //unity
        if (gotPackage)
        {
            
            if (nest.position.x > transform.position.x)
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            else
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            
            if (rigidbody2D.velocity.x < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (rigidbody2D.velocity.x > 0.1)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (gotPackage)
        {
            animator.SetBool("hasBox", true);
        }
        else
        {
            animator.SetBool("hasBox", false);
        }
        if (!PlayerController.isCarrying)
        {
            isAttacking = false;
        }
        if (!isAttacking && !isGrabbing)
        {
            animator.SetBool("isDiving", false);
        }
        //Debug.Log(rigidbody2D.velocity.x);
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isAttacking && PlayerController.isCarrying)
        {
            speed = 5;
            Debug.Log("test");
            isAttacking = true;
            //gameObject.transform.Rotate(0, 0, 35);
        }
        if (other.CompareTag("Package") && !isAttacking  && !isGrabbing && !gotPackage && Mathf.Abs(package.transform.position.y -transform.position.y) > 1 && !delivery)
        {
            speed = 5;
            isGrabbing = true;
            //gameObject.transform.Rotate(0, 0, 35);
            Debug.Log("grabbing!");
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && PlayerController.isCarrying && !gotPackage)
        {
            gotPackage = true;
            animator.SetBool("isDiving", false);
            PlayerController.isCarrying = false;
            isAttacking = false;
            //gameObject.transform.Rotate(0, 0, -35);
        }
        if (other.gameObject.CompareTag("nest") && gotPackage)
        {
            gotPackage = false;
            animator.SetBool("isDiving", false);
            package.transform.position = new Vector3 (nest.position.x,10,0f);
            package.GetComponent<SpriteRenderer>().enabled = true;
            package.GetComponent<BoxCollider2D>().enabled = true;
            package.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            delivery = true;
        }
        if (other.gameObject.CompareTag("Package"))
        {
            gotPackage = true;
            isGrabbing = false;
            animator.SetBool("isDiving", false);
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //gameObject.transform.Rotate(0, 0, -35);
            
        }
    }
}
