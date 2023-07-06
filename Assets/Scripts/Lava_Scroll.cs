using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lava_Scroll : MonoBehaviour
{

    public GameObject original;
    public float scale;
    private GameObject copy1, copy2;
    private SpriteRenderer spriteRenderer;
    private float spriteHeight;
    private float offset = 0.1f;
    private float speed = 0.5f;
    private float startPos;
    private bool gravity = true;

    public Text count;
    private int currentScore;

    void Start()
    {
        startPos = transform.position.y;

        spriteRenderer = original.GetComponent<SpriteRenderer>();
        spriteHeight = spriteRenderer.bounds.size.y;

        copy1 = Instantiate(original);
        copy1.transform.position = new Vector3(original.transform.position.x, original.transform.position.y-spriteHeight+offset, original.transform.position.z);
        copy1.transform.localScale = Vector3.one * scale + Vector3.one * offset;
        copy1.transform.parent = gameObject.transform;

        copy2 = Instantiate(original);
        copy2.transform.position = new Vector3(original.transform.position.x, original.transform.position.y+spriteHeight-offset, original.transform.position.z);
        copy2.transform.localScale = Vector3.one * scale + Vector3.one * offset;
        copy2.transform.parent = gameObject.transform;

        transform.position = new Vector3(transform.position.x, startPos + spriteHeight/2, transform.position.z);
    }

    void Update()
    {
        if(Input.GetKeyDown ("space") && currentScore > 0) {
            gravity = !gravity;
        }

        currentScore = int.Parse(count.text);

        if(gravity) {
            if(transform.position.y <= startPos - spriteHeight/2) {
                transform.position = new Vector3(transform.position.x, transform.position.y + spriteHeight - offset*2, transform.position.z);
            }
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        if(!gravity) {
            if(transform.position.y >= startPos + spriteHeight/2) {
                transform.position = new Vector3(transform.position.x, transform.position.y - spriteHeight + offset*2, transform.position.z);
            }
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
    /*void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(new Vector3(original.transform.position.x, original.transform.position.y - spriteHeight/2, original.transform.position.z), 0.3f);
    }*/
}
