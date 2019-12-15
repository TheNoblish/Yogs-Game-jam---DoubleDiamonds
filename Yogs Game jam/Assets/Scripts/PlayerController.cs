using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;

    public Text coinCounter;
    private GameObject coinHandler;

    float inputVertical;

    bool isClimbing;
    bool isMoving;
    public bool isJumping;
    public static bool isCarrying;
    bool onPackage;
    public bool canPet;
    public float speed = 5;
    public float jumpHeight = 2;
    public Transform jumpChecker;
    public GameObject package;
    public  float currentSpeed;
    private float currentJumpHeight;
    public float stepInterval = 5f;

    public LayerMask ladder;
    public float raycastDistance;

    public GameObject currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coinHandler = GameObject.Find("UI");
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
            currentJumpHeight = jumpHeight / 2;

            if (Input.GetKey("q") || Input.GetKey("right shift"))
            {
                //package.SetActive(true);
                package.GetComponent<SpriteRenderer>().enabled = true;
                package.GetComponent<BoxCollider2D>().enabled = true;
                package.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                package.transform.position = new Vector3(transform.position.x +2, transform.position.y, transform.position.z);
                isCarrying = false;
            }
        }
        else
        {
            animator.SetBool("hasPackage", false);
            currentSpeed = speed;
            currentJumpHeight = jumpHeight;
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

        if (other.CompareTag("Coin"))
        {
            other.GetComponent<AudioSource>().Play();
            coinHandler.GetComponent<CoinShop>().addCoin();
            other.GetComponent<SpriteRenderer>().enabled = false;
            other.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (other.CompareTag("Ground"))
        {
            isJumping = false;
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

        if (other.CompareTag("Ground"))
        {
            isJumping = true;
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
        if (Input.GetKeyDown("space") && !isJumping)
        {
            //rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpHeight);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, currentJumpHeight), ForceMode2D.Impulse);
            //jumping animation
        }

        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance, ladder);

        if (raycastHit2D.collider != null)
        {
            if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
            {
                isClimbing = true;

            }
        } else
        {
            isClimbing = false;
        }

        if (isClimbing == true && raycastHit2D.collider != null)
        {
            inputVertical = Input.GetAxisRaw("Vertical");

            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, inputVertical * speed);
            rigidbody2d.gravityScale = 0;
        } else
        {

            rigidbody2d.gravityScale = 5;
        }

        float prevSpeed = 1f; 

        if (inputVertical > 0 && isClimbing)
        {
            animator.SetBool("isClimbing", true);
            animator.speed = prevSpeed;
        } else if (inputVertical <= 0 && isClimbing)
        {
            prevSpeed = animator.speed;
            animator.speed = 0;
        } else
        {
            animator.speed = prevSpeed;
            animator.SetBool("isClimbing", false);
        }
    }

    public void setMovementSpeed(float newSpeed)
    {
        speed = speed + 1;
    }

    public void setJumpHeight(float newHeight)
    {
        jumpHeight = jumpHeight + 1;
    }

    public void setThrowDistance(float newDistance)
    {
        speed = speed + 1;
    }
}
