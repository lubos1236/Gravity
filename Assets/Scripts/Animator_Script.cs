using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Script : MonoBehaviour
{

    private Animator anim;
    private bool run = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Time.timeScale == 1f)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey("left") || Input.GetKey("right") || run)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
        
    }

    public void Run()
    {
        run = true;
    }

    public void Idle()
    {
        run = false;
    }
}
