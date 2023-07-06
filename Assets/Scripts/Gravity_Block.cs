using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_Block : MonoBehaviour
{

    private GameObject Player;
    private Rigidbody2D rb;
    public float speed = 2.0f;
    private int gravity = 1;
    public Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");

        //Rock block ignoruje kolízie s Playerom
        if (gameObject.tag == "Rock")
        {
            Physics2D.IgnoreCollision(Player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void Update()
    {

        gravity = Player.GetComponent<Player_Movement>().gravity;

        if(gravity % 2 != 0) {
            movement = new Vector2(0, -10);
        }
        if(gravity % 2 == 0) {
            movement = new Vector2(0, 10);
        }

        moveBlock(movement);
        
    }

    void moveBlock(Vector2 direction) {
        rb.AddForce(direction * speed);
    }
}
