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

    Controller2D controller;

    bool doubleJump;

    bool isClimbing;
    bool isMoving;
    public bool doubleJumped;
    public bool isJumping;
    public static bool isCarrying;
    bool onPackage;
    public bool canPet;
    public float speed = 5;
    public float baseSpeed;


    public Transform jumpChecker;
    public GameObject package;
    public  float currentSpeed;
    private float currentJumpHeight;
    public float stepInterval = 5f;

    //public float jumpHeight = 4;
    public float maxJumpHeight = 6;
    public float baseJumpHeight;
    public float minJumpHeight = 2;
    public float timeToJumpApex = .4f;
    float gravity;
    //public float jumpVelocity;
    public float maxJumpVelocity;
    public float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;

    public LayerMask ladder;
    public float raycastDistance;

    public GameObject currentEnemy;
    public GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coinHandler = GameObject.Find("UI");

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        print("Gravity: " + gravity + " Jump Velocity: " + maxJumpVelocity);

        baseSpeed = speed;
        baseJumpHeight = maxJumpHeight;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.x > 0)
        {
            isMoving = true;
            spriteRenderer.flipX = true;
            animator.SetBool("isRunning", true);
            animator.SetBool("isFacingRight", true);

            if (isCarrying)
            {
                animator.SetBool("hasPackage", true);
            } else
            {
                animator.SetBool("hasPackage", false);
            }

        } else if (input.x < 0)
        {
            isMoving = true;
            spriteRenderer.flipX = false;
            animator.SetBool("isRunning", true);
            animator.SetBool("isFacingRight", false);

            if (isCarrying)
            {
                animator.SetBool("hasPackage", true);
            }
            else
            {
                animator.SetBool("hasPackage", false);
            }

        } else if (input.x == 0)
        {
            isMoving = false;
            animator.SetBool("isRunning", false);

            if (isCarrying)
            {
                animator.SetBool("hasPackage", true);
            }
            else
            {
                animator.SetBool("hasPackage", false);
            }
        }

        float targetVelocityX = input.x * speed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            doubleJump = true;
        } else if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
        {
            doubleJump = false;
            velocity.y = maxJumpVelocity;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }

        }

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
                animator.SetBool("isPetting", true);
                Debug.Log("pet");
                currentEnemy.GetComponent<enemyController>().pets++;
            }
            else
                animator.SetBool("isPetting", false);
        }
        else
            animator.SetBool("isPetting", false);

        // change speed while carrying and allow dropping the package
        if (isCarrying)
        {
            animator.SetBool("hasPackage", true);
            speed = baseSpeed / 1.5f;
            maxJumpHeight = baseJumpHeight * 2f;
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);

            if (Input.GetKey("q") || Input.GetKey("right shift"))
            {
                //package.GetComponent<PackageSounds>().Step();
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
            speed = baseSpeed;
            maxJumpHeight = baseJumpHeight;
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        }
        //if (Input.GetKeyDown("space") && !isJumping)
        //{
        //    //rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpHeight);
        //    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, currentJumpHeight), ForceMode2D.Impulse);
        //    //jumping animation


        //}
        //else if (Input.GetKeyDown("space") && isJumping && !doubleJumped && !isCarrying)
        //{
        //    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, currentJumpHeight), ForceMode2D.Impulse);
        //    doubleJumped = true;
        //}
        //if (isJumping && !isClimbing)
        //{
        //    animator.SetBool("isJumping", true);
        //}
        //else
        //{
        //    animator.SetBool("isJumping", false);
        //}

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

        if (other.CompareTag("SilverCoin"))
        {
            other.GetComponent<AudioSource>().Play();
            coinHandler.GetComponent<CoinShop>().addCoin(1);
            other.GetComponent<SpriteRenderer>().enabled = false;
            other.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (other.CompareTag("GoldCoin"))
        {
            other.GetComponent<AudioSource>().Play();
            coinHandler.GetComponent<CoinShop>().addCoin(2);
            other.GetComponent<SpriteRenderer>().enabled = false;
            other.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (other.CompareTag("Ground"))
        {
            isJumping = false;
            doubleJumped = false;
        }

        if (other.CompareTag("End"))
        {
            if (isCarrying) {
                UI.GetComponent<OutroCutscene>().StartOutro();
            } else
            {
                return;
            }

        }

        if (other.CompareTag("NPC"))
        {
            //rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX;
            other.GetComponent<NPCManager>().startDialogue();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.CompareTag("NPC"))
        //{
        //    //rigidbody2d.constraints = RigidbodyConstraints2D.FreezePositionX;
        //    other.gameObject.GetComponent<NPCManager>().startDialogue();
        //}
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Crate"))
        {
            Debug.Log("test2");
            if (Input.GetKeyDown("e") || Input.GetKey("right ctrl"))
            {
                Debug.Log("test");
                other.gameObject.GetComponent<CrateController>().OpenCrate();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

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

        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance, ladder);

        if (raycastHit2D.collider != null)
        {
            controller.collisions.above = false;
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                isClimbing = true;
                transform.Translate(new Vector2(0, 1f) * Time.deltaTime * speed);
                gravity = 0;          
            }
        }
        else
        {
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            isClimbing = false;
        }

        //    if (isClimbing == true && raycastHit2D.collider != null)
        //    {
        //        inputVertical = Input.GetAxisRaw("Vertical");

        //        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, inputVertical * speed);
        //        rigidbody2d.gravityScale = 0;
        //    }
        //    else
        //    {

        //        rigidbody2d.gravityScale = 5;
        //    }

        //    float prevSpeed = 1f;

        //    if (inputVertical > 0 && isClimbing)
        //    {
        //        animator.SetBool("isClimbing", true);
        //        animator.speed = prevSpeed;
        //    }
        //    else if (inputVertical <= 0 && isClimbing)
        //    {
        //        prevSpeed = animator.speed;
        //        animator.speed = 0;
        //    }
        //    else
        //    {
        //        animator.speed = prevSpeed;
        //        animator.SetBool("isClimbing", false);
        //    }
        //}
    }

        public void setMovementSpeed(float newSpeed)
        {
            speed = speed + 1;
        }

        public void setJumpHeight(float newHeight)
        {
            //jumpHeight = jumpHeight + 1;
        }

        public void setThrowDistance(float newDistance)
        {
            speed = speed + 1;
        }
    
}
