using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Animator_Script : MonoBehaviour
{

    public RectTransform[] layers;
    public RectTransform[] lava;

    private float speed = 0.3f;

    void Start()
    {
        //Time.timeScale = 8;
    }

    void FixedUpdate()
    {
        //MOUNTAINS MOVE
        layers[0].anchoredPosition -= new Vector2(0.8f * speed, 0);
        layers[1].anchoredPosition -= new Vector2(0.6f * speed, 0);
        layers[2].anchoredPosition -= new Vector2(0.4f * speed, 0);
        layers[3].anchoredPosition -= new Vector2(0.2f * speed, 0);
        
        //MOUNTAINS REPEAT
        for(int i = 0; i < layers.Length; i++) {
            if(i != 2 && layers[i].anchoredPosition.x < -(layers[i].sizeDelta.x / 2 * layers[i].localScale.x)) { //layer 3 má nepárny počet hôr takže potrebuje byť vydelený 3
                layers[i].anchoredPosition = new Vector2(0, 0);
            }
            if(i == 2 && layers[i].anchoredPosition.x < -(layers[i].sizeDelta.x / 3 * layers[i].localScale.x)) { //layer 3 má nepárny počet hôr takže potrebuje byť vydelený 3
                layers[i].anchoredPosition = new Vector2(0, 0);
            }
        }
        
        //LAVA MOVE
        for(int j = 0; j < lava.Length; j++) {
            //MOVE
            if(j == 0)
                lava[j].anchoredPosition -= new Vector2(0.7f * speed, 0.3f);
            if(j == 1)
                lava[j].anchoredPosition -= new Vector2(0.5f * speed, 0.3f);

            //REPEAT
            //X
            if(lava[j].anchoredPosition.x < -900) {
                lava[j].anchoredPosition = new Vector2(415, 0);
            }
            //Y
            if(lava[j].anchoredPosition.y < -(lava[j].sizeDelta.y / 5 * lava[j].localScale.y)) {
                lava[j].anchoredPosition = new Vector2(lava[j].anchoredPosition.x, 0);
            }
        }
    }
}
