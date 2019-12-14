using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;

    bool isMoving;
    bool isJumping;
    public static bool isCarrying;
    bool onPackage;
    public bool canPet;
    public float speed = 5;
    public float jumpHeight = 5;
    public Transform jumpChecker;
    public GameObject package;
    public  float currentSpeed;
    private float currentJumpHeight;
    public float stepInterval = 5f;

    public GameObject currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        isCarrying = true;
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (onPackage)
        {
            // pickup the package
            if (Input.GetKey("e") || Input.GetKey("right ctrl"))
            {
                isCarrying = true;
                //package.SetActive(false);
                package.GetComponent<SpriteRenderer>().enabled = false;
                package.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        if (canPet)
        {
            if (Input.GetKey("s") || Input.GetKey("down"))
            {
                Debug.Log("pet");
                currentEnemy.GetComponent<enemyController>().pets++;
            }
        }

        // change speed while carrying and allow dropping the package
        if (isCarrying)
        {
            animator.SetBool("hasPackage", true);
            currentSpeed = speed / 2;

            if (Input.GetKey("q") || Input.GetKey("right shift"))
            {
                //package.SetActive(true);
                package.GetComponent<SpriteRenderer>().enabled = true;
                package.GetComponent<BoxCollider2D>().enabled = true;
                package.transform.position = new Vector3(transform.position.x +2, transform.position.y, transform.position.z);
                isCarrying = false;
            }
        }
        else
        {
            animator.SetBool("hasPackage", false);
            currentSpeed = speed;
        }

    }

    // allow player to pikcup while on the package
    void OnTriggerEnter2D(Collider2D other)
    {
        // allow player to pikcup while on the package
        if (other.CompareTag("Package"))
            onPackage = true;
        // allow petting and set specific enemy being petted
        if (other.CompareTag("Enemy"))
        {
            canPet = true;
            currentEnemy = other.gameObject;
        }
        else if (other.CompareTag("enemyBoundary"))
        {
            canPet = true;
            currentEnemy = other.gameObject.transform.parent.gameObject;
        }

        if (other.CompareTag("NPC"))
        {
            //rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX;
            other.GetComponent<NPCManager>().startDialogue();
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        //stop being allowed to pickup when leaving the package
        if (other.CompareTag("Package"))
            onPackage = false;
        // stop allowing petting
        if (other.name == "groundEnemy")
        {
            canPet = false;
            currentEnemy = null;
        }
        else if (other.name == "enemyArea")
        {
            canPet = false;
            currentEnemy = null;
        }
    }

    private void FixedUpdate()
    {

        //If the player presses A or LEFT ARROW KEY
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            isMoving = true;
            spriteRenderer.flipX = false;
            animator.SetBool("isRunning", true);
            animator.SetBool("isFacingRight", false);
            rigidbody2d.velocity = new Vector2(-currentSpeed, rigidbody2d.velocity.y);

            if (!isJumping)
            {
                //left moving animation
            }
        }
        //Else if the player presses D or RIGHT ARROW KEY
        else if (Input.GetKey("d") || Input.GetKey("right"))
        {
            isMoving = true;
            spriteRenderer.flipX = true;
            animator.SetBool("isRunning", true);
            animator.SetBool("isFacingRight", true);
            rigidbody2d.velocity = new Vector2(currentSpeed, rigidbody2d.velocity.y);

            if (!isJumping)
            {
                //right moving animation
            }
        }
        //Else (when the player is not moving left or right)
        else
        {
            isMoving = false;
            animator.SetBool("isRunning", false);
            animator.SetBool("isFacingRight", true);
            //We set the velocity of x to 0 so the player isn't slip sliding around
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);

            if (!isJumping)
            {
                //idle animation
            }
        }

        //If the player presses SPACE
        if (Input.GetKey("space") && !isJumping)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpHeight);
            //jumping animation
        }

        if (Physics2D.Linecast(transform.position, jumpChecker.position, 1 << LayerMask.NameToLayer("Floor")))
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
            //jumping animation
        }
    }

}
