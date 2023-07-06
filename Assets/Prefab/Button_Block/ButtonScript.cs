using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private Transform Player;
    public GameObject Button_Tile;
    private float distance;
    Vector2 player;
    Vector2 button;
    private int treshold = 1;
    bool ESCPressed = false;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ESCPressed = !ESCPressed;
        }
        player = Player.position;
        button = transform.position;
        distance = Vector2.Distance(player, button);
        if(!ESCPressed)
        {
            if (distance < treshold && Input.GetKeyDown(KeyCode.E))
            {
                Button_Tile.gameObject.SetActive(false);
            }
        }
        
    }
}
