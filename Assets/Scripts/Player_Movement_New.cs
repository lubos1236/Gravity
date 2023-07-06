using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Movement_New : MonoBehaviour
{

    public float speed = 5.0f;
    public float rotationSpeed = 6;
    private float rotation = 0;
    public float gravityForce = 10.0f;
    private Rigidbody2D rb;
    private Vector2 direction;
    private int gravity = 1;
    public int currentScore;
    //public GameObject Camera;
    public Text count;
    private bool leftPressed = false, rightPressed = false;
    private bool HackMode = false;
    public bool finished = false; // na konci levelu 

    public bool isGrounded = false;
    public GameObject Child; //
    private Animator anim; //

    private CapsuleCollider2D boxCollider2D; //
    [SerializeField] private LayerMask platformLayerMask; //

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.gravity = new Vector2(0, -gravityForce);
        //Camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        boxCollider2D = GetComponent<CapsuleCollider2D>(); //
        anim = Child.GetComponent<Animator>(); //
    }

    void Update()
    {
        //HACK MODE
        if (Input.GetKeyDown("h"))
        {
            HackMode = !HackMode;
            currentScore = 1000000000;
        }

        if (Time.timeScale >= 0 && finished==false)
        {
            //RESTART
            if (Input.GetKey("r"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            //GRAVITACIA
            if (Input.GetKeyDown("space") && currentScore > 0)
            {
                gravity++;
                currentScore--;
            }

            if (gravity % 2 != 0)
                //Physics2D.gravity = new Vector2(0, -gravityForce);
                Physics2D.gravity = Vector2.Lerp(Physics2D.gravity, Vector2.down * gravityForce, Time.deltaTime * 5);
            if (gravity % 2 == 0)
                //Physics2D.gravity = new Vector2(0, gravityForce);
                Physics2D.gravity = Vector2.Lerp(Physics2D.gravity, Vector2.down * -gravityForce, Time.deltaTime * 5);

            //TEXT
            count.text = currentScore.ToString();

            //ROTACIA
            if (gravity % 2 == 0 && rotation != 180)
            {
                if (rotation > 179)
                    rotation = 180;
                else
                    rotation += Time.deltaTime * rotationSpeed;
            }
            if (gravity % 2 != 0 && rotation != 0)
            {
                if (rotation < 1)
                    rotation = 0;
                else
                    rotation -= Time.deltaTime * rotationSpeed;
            }
            transform.rotation = Quaternion.Euler(0, 0, rotation);

            //POHYB
            if (Input.GetKey("a") || Input.GetKey("left") || leftPressed == true)
            {
                direction = new Vector2(-1, 0);

            }
            else if (Input.GetKey("d") || Input.GetKey("right") || rightPressed == true)
            {
                direction = new Vector2(1, 0);
            }
            else
            {
                direction = Vector2.zero;
            }
        }
        //Camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    void FixedUpdate()
    {
        float extraHeight = 0.9f;
        float extraWidth = 0.1f;
        RaycastHit2D rayUp = Physics2D.Raycast(boxCollider2D.bounds.center, new Vector2(0, 1), boxCollider2D.bounds.size.y/2 + extraHeight, platformLayerMask);
        Color upColor;
        if (rayUp.collider != null)
            upColor = Color.green;
        else
            upColor = Color.red;

        RaycastHit2D rayDown = Physics2D.Raycast(boxCollider2D.bounds.center, new Vector2(0, -1), boxCollider2D.bounds.size.y/2 + extraHeight, platformLayerMask);
        Color downColor;
        if (rayDown.collider != null)
            downColor = Color.green;
        else
            downColor = Color.red;

        RaycastHit2D rayLeft = Physics2D.Raycast(boxCollider2D.bounds.center, new Vector2(-1, 0), boxCollider2D.bounds.size.x/2 + extraWidth, platformLayerMask);
        Color leftColor;
        if (rayLeft.collider != null)
            leftColor = Color.green;
        else
            leftColor = Color.red;

        RaycastHit2D rayRight = Physics2D.Raycast(boxCollider2D.bounds.center, new Vector2(1, 0), boxCollider2D.bounds.size.x/2 + extraWidth, platformLayerMask);
        Color rightColor;
        if (rayRight.collider != null)
            rightColor = Color.green;
        else
            rightColor = Color.red;

        Debug.DrawRay(boxCollider2D.bounds.center, new Vector2(0, boxCollider2D.bounds.size.y/2 + extraHeight), upColor);
        Debug.DrawRay(boxCollider2D.bounds.center, new Vector2(0, -boxCollider2D.bounds.size.y/2 - extraHeight), downColor);
        Debug.DrawRay(boxCollider2D.bounds.center, new Vector2(-boxCollider2D.bounds.size.x/2 - extraWidth, 0), leftColor);
        Debug.DrawRay(boxCollider2D.bounds.center, new Vector2(boxCollider2D.bounds.size.x/2 + extraWidth, 0), rightColor);

        //ANIMACIA SA VO VZDUCHU VYPNE
        if (gravity % 2 == 0 && rayUp.collider == null || gravity % 2 != 0 && rayDown.collider == null)
        {
            anim.enabled = false;
            Child.transform.localPosition = Vector3.Lerp(Child.transform.localPosition,Vector3.zero, 0.1f);
            Child.transform.localRotation = Quaternion.Lerp(Child.transform.localRotation, Quaternion.identity, 0.1f);
            Child.transform.localScale = Vector3.one;
        } else
        {
            anim.enabled = true;
        }

        //OPRETIE O STENU
        if (gravity % 2 != 0 && rayLeft.collider != null && rayLeft.collider.tag != "Blue_Block" || gravity % 2 == 0 && rayRight.collider != null && rayRight.collider.tag != "Blue_Block")
        {
            anim.enabled = false;
            Child.transform.localPosition = Vector3.Lerp(Child.transform.localPosition, new Vector3(-0.551f, 0.01f, 0.553f), 0.5f);
            Child.transform.localRotation = Quaternion.Lerp(Child.transform.localRotation, Quaternion.Euler(23.8f, -30.9f, 0f), 0.5f);
            Child.transform.localScale = Vector3.Lerp(Child.transform.localScale, new Vector3(1.13f, 1.093f, 1.0f), 0.5f);
        }
        if (gravity % 2 != 0 && rayRight.collider != null && rayRight.collider.tag != "Blue_Block" || gravity % 2 == 0 && rayLeft.collider != null && rayLeft.collider.tag != "Blue_Block")
        {
            anim.enabled = false;
            Child.transform.localPosition = Vector3.Lerp(Child.transform.localPosition, new Vector3(0.551f, -0.01f, -0.553f), 0.5f);
            Child.transform.localRotation = Quaternion.Lerp(Child.transform.localRotation, Quaternion.Euler(23.8f, 30.9f, 0f), 0.5f);
            Child.transform.localScale = Vector3.Lerp(Child.transform.localScale, new Vector3(1.13f, 1.093f, 1.0f), 0.5f);
        }

        //SQUASH ANIMACIA
        if ((rb.velocity.y > 15 || rb.velocity.y < -15) && gravity % 2 != 0 && rayDown.collider != null)
        {
            anim.SetTrigger("squash");
        }
        if ((rb.velocity.y > 15 || rb.velocity.y < -15) && gravity % 2 == 0 && rayUp.collider != null)
        {
            anim.SetTrigger("squash");
        }
        
        Move(direction);

        if (rb.velocity.x > speed || rb.velocity.x < -speed) //Obmedzenie vertikálnej rýchlosti
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speed, speed), rb.velocity.y);
        }
    }

    void Move(Vector2 _direction)
    {
        rb.AddForce(_direction * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Orb")
        {
            currentScore++;
        }
        if (collision.gameObject.tag == "Bullet" && HackMode == false) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Red" && HackMode == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(collision.gameObject.name == "Tilemap_Green")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.name == "Tilemap_Green")
        {
            isGrounded = false;
        }
    }

    public void LeftButtonDown()
    {
        leftPressed = true;
    }

    public void LeftButtonUp()
    {
        leftPressed = false;
    }

    public void RightButtonDown()
    {
        rightPressed = true;
    }

    public void RightButtonUp()
    {
        rightPressed = false;
    }

    public void ChangeGravity()
    {
        if (currentScore > 0)
        {
            gravity++;
            currentScore--;
        }
    }
}
