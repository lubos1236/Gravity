using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Turret : MonoBehaviour
{
    
    private Vector2 playerPos;
    private Vector3 currentDirection;
    private Vector3 nextDirection;
    private Vector3 direction;
    private float angle = 0;
    public float followSpeed = 0.03f;

    public float tresholdRadius; //
    private Animator anim; //
    public Transform glowRed, glowWhite; //
    private Vector3 redSize, whiteSize; //

    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        anim.enabled = false;

        redSize = glowRed.localScale;
        whiteSize = glowWhite.localScale;
        glowRed.localScale = Vector3.zero;
        glowWhite.localScale = Vector3.zero;
    }

    void Update()
    {
        if (Vector2.Distance(new Vector2(Player.transform.position.x, Player.transform.position.y), new Vector2(transform.position.x, transform.position.y)) < tresholdRadius)
        {
            anim.enabled = true;
            anim.SetBool("open", true);
            Grow();
        } else
        {
            anim.SetBool("open", false);
            Shrink();
        }

        nextDirection = Player.transform.position - transform.position;
        
        currentDirection = Vector3.Slerp(currentDirection, nextDirection, followSpeed);

        angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, -Vector3.back);
    }

    void Grow()
    {
        if (glowRed.localScale.x < redSize.x)
        {
            glowRed.localScale = Vector2.Lerp(glowRed.localScale, redSize, 0.008f);
            glowWhite.localScale = Vector2.Lerp(glowWhite.localScale, whiteSize, 0.008f);
        }
    }

    void Shrink()
    {
        if (glowRed.localScale.x < redSize.x)
        {
            glowRed.localScale = Vector2.Lerp(glowRed.localScale, Vector3.zero, 0.005f);
            glowWhite.localScale = Vector2.Lerp(glowWhite.localScale, Vector3.zero, 0.005f);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, tresholdRadius);
    }
}
