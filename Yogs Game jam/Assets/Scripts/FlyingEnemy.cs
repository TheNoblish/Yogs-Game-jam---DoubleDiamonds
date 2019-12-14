using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    Animator animator;

    GameObject player;
    GameObject package;
    bool returned = true;
    Rigidbody2D rigidbody2D;
    int patrolDirection = -1;

    Vector2 playerPosition;

    public bool isAttacking;
    public bool gotPackage;
    public float speed;
    public float attackRange;
    public Transform nest;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player 1");
        package = GameObject.Find("package");
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && !gotPackage)
        {
            animator.SetBool("isDiving", false);
            rigidbody2D.velocity = new Vector2(1.5f*patrolDirection, 0) * speed;
        } else if (isAttacking)
        {

            animator.SetBool("isDiving", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (gotPackage)
        {
            animator.SetBool("isDiving", false);
            
                transform.position = Vector2.MoveTowards(transform.position, nest.position, speed * Time.deltaTime);
            
        }

        if (Mathf.Abs(nest.position.x - transform.position.x) > 40 && returned)
        {
            patrolDirection = 1;
            returned = false;
        }
        else if (Mathf.Abs(nest.position.x - transform.position.x) < 5)
            patrolDirection = -1;
            returned = true;


        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isAttacking && PlayerController.isCarrying)
        {
            speed = 5;
            Debug.Log("test");
            isAttacking = true;
            gameObject.transform.Rotate(0, 0, 35);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && PlayerController.isCarrying)
        {
            gotPackage = true;
            PlayerController.isCarrying = false;
            isAttacking = false;
        }
        if (other.gameObject.CompareTag("nest") && gotPackage)
        {
            gotPackage = false;
            animator.SetBool("isDiving", false);
            package.transform.position = new Vector3 (nest.position.x,nest.position.y+1,0f);
            package.GetComponent<SpriteRenderer>().enabled = true;
            package.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
