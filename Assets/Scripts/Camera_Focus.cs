using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Focus : MonoBehaviour
{

    private GameObject camera;
    private Camera cam;
    private Rigidbody2D rb;
    private float velocityMag;
    private float minVelocity = 13.0f;
    private float maxVelocity = 28.0f;
    private float mixValue = 0.0f;
    private float camSize;
    private float minZoom = 5.5f;
    private float maxZoom = 11.0f;
    Player_Movement_New playerScript;
    private bool grounded = false;
    private bool capReached = false; 

    //focal object array
    public Transform[] focalObjects;
    private int objectsCount;
    public float distThreshold = 10.0f;
    private Vector3 midPos;
    private bool camLocked = true;

    void Start()
    {
        camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        cam = camera.GetComponent<Camera>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        camSize = minZoom;
        
        playerScript = GetComponent<Player_Movement_New>();
        objectsCount = focalObjects.Length;

    }

    void FixedUpdate()
    {

        grounded = playerScript.isGrounded;
        if (grounded == true && capReached == true)
            capReached = false;

        if (capReached == false)
            velocityMag = rb.velocity.magnitude;
            velocityMag = Mathf.Clamp(velocityMag, minVelocity, maxVelocity);
            mixValue = Mathf.InverseLerp(minVelocity, maxVelocity, velocityMag);
        if (capReached == true)
            mixValue = 1;

        if (grounded == false && mixValue == 1)
            capReached = true;

        //Debug.Log(mixValue);

        camSize = Mathf.Lerp(minZoom, maxZoom, mixValue);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camSize, 0.08f); //smoothing

        //Distance
        if (objectsCount > 0) {
            if (Vector3.Distance(gameObject.transform.position, focalObjects[0].position) <= distThreshold) {
                camLocked = false;
                midPos = Vector3.Lerp(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10), new Vector3(focalObjects[0].position.x, focalObjects[0].position.y, -10), 0.3f);
                camera.transform.position = Vector3.Lerp(camera.transform.position, midPos, 0.1f);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, maxZoom, 0.1f);
            }
        } else {
            if (camLocked == false) {
                camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(transform.position.x, transform.position.y, -10), 0.2f);

                if (Vector3.Distance(camera.transform.position, new Vector3(transform.position.x, transform.position.y, -10)) < 0.1f) {
                    camLocked = true;
                }
            } else if (camLocked == true) {
                camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }
        }
        Debug.Log(objectsCount);
        

        /*if(grounded)
            camSize = minZoom;
        else if(!grounded)
            camSize = maxZoom;
            
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camSize, 0.08f); //smoothing
        */
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(focalObjects[0].position, distThreshold);
    }
}
