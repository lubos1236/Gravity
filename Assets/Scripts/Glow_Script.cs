using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow_Script : MonoBehaviour
{
    
    public GameObject target;
    private float offset = 1.2f; 
    private float currentScale;
    private float maxScale;
    private float minScale;
    private float random;
    private float multiply;

    void Start()
    {
        transform.position = target.transform.position;
        currentScale = transform.localScale.x;
        maxScale = currentScale + offset/2;
        minScale = currentScale - offset/2;
        random = Random.Range(0.0f, 100.0f);
    }

    void FixedUpdate()
    {
        if(target == null)
            Destroy(gameObject);

        float noise = Mathf.PerlinNoise(random, Time.time * 2);
        multiply = Mathf.Lerp(minScale, maxScale, noise);
        transform.localScale = new Vector3(multiply, multiply, 1);
    }

}
