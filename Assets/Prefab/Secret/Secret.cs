using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Secret : MonoBehaviour
{

    private Tilemap tilemap;
    Color c =  new Color(1f, 1f, 1f, 1f);

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            c.a = 0.2f;
            tilemap.color = c;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            c.a = 1f;
            tilemap.color = c;
        }
    }
}
