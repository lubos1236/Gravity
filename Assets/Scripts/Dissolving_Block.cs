using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolving_Block : MonoBehaviour
{

    public float timer1 = 0.5f;
    public int timer2 = 5;
    private static bool dissolve = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(dissolve) {
            Dissolve();
            StopCoroutine("Dissolve");
            StartCoroutine("Dissolve");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            Dissolve();
        }
    }

    void Dissolve() {
        Invoke("DeactivateObject", timer1);
        Invoke("ActivateObject", timer2);
        Switch();
    }

    void DeactivateObject() {
        gameObject.SetActive(false);
    }

    void ActivateObject() {
        gameObject.SetActive(true);
    }

    void Switch() {
        dissolve = false;
    }
}
