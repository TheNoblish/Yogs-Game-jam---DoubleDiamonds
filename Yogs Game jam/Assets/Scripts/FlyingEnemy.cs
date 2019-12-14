using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    Animator animator;

    GameObject player;

    Rigidbody2D rigidbody2D;

    Vector2 playerPosition;

    bool isAttacking;

    public float speed;
    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            animator.SetBool("isDiving", false);
            rigidbody2D.velocity = new Vector2(-1.5f, 0) * speed;
        } else if (isAttacking)
        {

            animator.SetBool("isDiving", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }


        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isAttacking)
        {
            speed = 5;
            Debug.Log("test");
            isAttacking = true;
            gameObject.transform.Rotate(0, 0, 35);
        }
    }
}
