using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sectret_Script : MonoBehaviour
{

    public  GameObject prefab;
    private GameObject animObject;
    private GameObject group;
    private float timer = 0;
    private bool start = false;
    public bool SecretColected = false;
    private GameObject starGlow;
    RectTransform rt;
    private float glowScale = 1;


    void Start()
    {
        starGlow = GameObject.FindGameObjectWithTag("Star_Glow");
        rt = starGlow.GetComponent<RectTransform>();
        starGlow.SetActive(false);
    }

    void Update()
    {
        if (start)
        {
            starGlow.SetActive(true);
            rt.localScale = new Vector3(glowScale, glowScale, 1);
            glowScale = Mathf.Lerp(glowScale, 3, 0.001f);

            timer += Time.deltaTime;
            if (timer > 4.0f)
            {
                Destroy(animObject);
                Destroy(group);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //gameObject.SetActive(false);
            transform.localScale = new Vector3(0, 0, 0);
            Animate();
            SecretColected = true;

        }
    }

    void Animate()
    {
        group = new GameObject("GroupForStarAnim");
        animObject = Instantiate(prefab);
        animObject.transform.parent = group.transform;
        group.transform.position = transform.position;
        group.gameObject.transform.localScale = Vector3.one * 0.5f;

        start = true;
    }
}
