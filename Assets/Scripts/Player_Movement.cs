using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    public static bool leftPressed = false;
    public static bool rightPressed = false;
    public GameObject Left_Button;  
    public GameObject Right_Button;
    public GameObject Camera;
    public GameObject Gravity_Manager;
    public int rotationSpeed; //definuje sa v unity (Ideálne 10)
    float rotation = 0;
    public int gravity = 1; //nepárne = pustená graviácia; párne = vypnutá gravitácia
    public GameObject Button;
    private Rigidbody2D rb;
    public float Y_speed = 2.0f, X_speed = 2f;
    public Vector2 Y_movement, X_movement;

    public Text count;
    public int currentScore; //NASTAVENIE V UNITY
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Button buttonL = Left_Button.GetComponent<Button>();
        Button buttonR = Right_Button.GetComponent<Button>();
        //Gravity_Manager gm = gameObject.GetComponent<Gravity_Manager>();  //pridané DuŠanom
        //public Gravity_Manager gm;

    }

    void Update()
    {
       
        if (Input.GetKey("r"))
            {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
            //Gravitacia

            if (Input.GetKeyDown("space") && currentScore > 0)
        {
            gravity++;
            currentScore--;
        }

        if (gravity % 2 == 0)
        {
            Button.GetComponent<Image>().color = Color.white;
        }
        else if (gravity % 2 != 0)
        {
            Button.GetComponent<Image>().color = Color.red;
        }

        count.text = currentScore.ToString();


        //ROTÁCIA
        if (gravity % 2 == 0 && rotation != 180) {
            rotation += rotationSpeed;
        }

        if(gravity % 2 != 0 && rotation != 0) {
            rotation -= rotationSpeed;
        }
        
        transform.rotation = Quaternion.Euler(0,0,rotation);
        //Camera.transform.rotation = Quaternion.Euler(0,0,rotation);  //Kamera sa otáča s hráčom

        //HORE/DOLE
        if(gravity % 2 != 0)
        Y_movement = new Vector2(0, -10); //Pohyb dole
        if(gravity % 2 == 0)
        Y_movement = new Vector2(0, 10); //Pohyb hore
        moveY(Y_movement);

        //POHYB <- / ->
        if ((Input.GetKey("a") || Input.GetKey("left")) && gravity % 2 != 0) {
            X_movement = new Vector2(-5, 0);
            MoveLeft(X_movement);
        } else if((Input.GetKey("a") || Input.GetKey("left")) && gravity % 2 == 0) {
            X_movement = new Vector2(-5, 0);
            MoveLeft(X_movement);
        } else if((Input.GetKey("d") || Input.GetKey("right")) && gravity % 2 != 0) {
            X_movement = new Vector2(5, 0);
            MoveRight(X_movement);
        } else if ((Input.GetKey("d") || Input.GetKey("right")) && gravity % 2 == 0) {
            X_movement = new Vector2(5, 0);
            MoveRight(X_movement);
        } else if(leftPressed) {
            X_movement = new Vector2(-5, 0);
            MoveLeft(X_movement);
        } else if(rightPressed) {
            X_movement = new Vector2(5, 0);
            MoveRight(X_movement);
        }

        Camera.transform.position = new Vector3(transform.position.x,transform.position.y,-10); //Kamera kopíruje pozíciu hráča
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Orb")
        {
            //pridat 1 ku gravity v Gravity_Manager
            Add();

        }
    }

    public void moveY(Vector2 Y_direction) {
        rb.AddForce(Y_direction * Y_speed);
    }

    public void MoveLeft(Vector2 X_direction) {
        rb.AddForce(X_direction * X_speed);
    }

    public void MoveRight(Vector2 X_direction) {
        rb.AddForce(X_direction * X_speed);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Red") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LeftButtonDown() {
        leftPressed = true;
    }

    public void LeftButtonUp() {
        leftPressed = false;
    }

    public void RightButtonDown() {
        rightPressed = true;
    }

    public void RightButtonUp() {
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
    public void Add()
    {
        currentScore++;
    }

}
