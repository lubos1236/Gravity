using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject bulletPrefab;
    public GameObject glowPrefab;
    private GameObject glow;
    //private readonly float delayShoot = 0.5f;
    [Range(0.3f,1.0f)]
    public float delayShoot = 0.5f;
    private float timeElapsed;
    private bool canShoot = true;
    private float minScale = 2.0f, maxScale = 4.0f, currentScale;

    void Start()
    {
        glow = Instantiate(glowPrefab);
        glow.transform.position = transform.position;
        glow.transform.localScale = Vector3.one * maxScale;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 0.8f, transform.position.y), new Vector2(-1, 0));
        //Debug.DrawLine(new Vector2(transform.position.x - 0.8f, transform.position.y), hit.point, Color.red);
        if (hit.collider.tag == "Blue_Block" && hit.distance < 0.5f)
        {
            canShoot = false;
            timeElapsed = 0;
        }
        
        if (canShoot==true)
        {
            Shoot();
            canShoot = false;
        }
        if (canShoot==false)
        {
            timeElapsed += Time.deltaTime;
        }
        if (timeElapsed > delayShoot)
        {
            canShoot = true;
            timeElapsed = 0f;
        }

        currentScale = Mathf.Lerp(minScale, maxScale, timeElapsed * 2);
        glow.transform.localScale = Vector3.one * currentScale;

    }
    void Shoot()
    {
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
    }
}
