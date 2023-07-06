using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laser : MonoBehaviour
{

    //private LineRenderer lineRenderer;
    public Transform LaserHit;
    
    public Transform glowRed; //
    private Vector3 redSize; //
    public bool isActive = true; //
    private SpriteRenderer sr;
    private bool HackMenu = false;

    public GameObject FX; //

    void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.enabled = true;
        //lineRenderer.useWorldSpace = true;

        sr = GetComponent<SpriteRenderer>();
        redSize = glowRed.localScale;
    }

    void Update()
    {
        //HACK MENU
        if (Input.GetKeyDown("h")) {
            HackMenu = !HackMenu;
        }

        if (glowRed.localScale.x > redSize.x/1.5)
        {
            isActive = true;
        } else
        {
            isActive = false;
        }

        if (isActive)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
            //Debug.DrawLine(transform.position, hit.point);
            LaserHit.position = hit.point;
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, LaserHit.position);
            //sr.size = new Vector2(hit.distance, 1);
            gameObject.transform.localPosition = new Vector3(LaserHit.localPosition.x/2, 0, 0);
            gameObject.transform.localScale = new Vector3(LaserHit.localPosition.x, 1, 1);

            //FX
            FX.SetActive(true);
            FX.transform.position = hit.point;

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player" && HackMenu == false)
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                }
            }
        } else
        {
            FX.SetActive(false);
        }
    }
}
