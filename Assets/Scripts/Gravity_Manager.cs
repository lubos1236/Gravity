using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gravity_Manager : MonoBehaviour
{
    public int gravity = 1;
    public GameObject Button;

    public Text count;
    public int currentScore; //NASTAVENIE V UNITY


    void Update()
    {

        if(Input.GetKeyDown ("space") && currentScore > 0){
            ChangeGravity();
        }

        
    }

    public void ChangeGravity()
    {
        gravity++;
        currentScore--;
        if (gravity % 2 == 0)
        {
            Button.GetComponent<Image>().color = Color.white;
        }
        else if (gravity % 2 != 0)
        {
            Button.GetComponent<Image>().color = Color.red;
        }

        count.text = currentScore.ToString();
    }

    public void Add()
    {
        currentScore++;
    }
    }
