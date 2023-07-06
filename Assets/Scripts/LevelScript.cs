using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public Sprite ButtonLock;
    public Text text;
    [Space]
    public Stars starObj;
    public GameObject Stars;
    public Sprite[] starSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (!starObj.LevelUnlocked)
        {
            this.GetComponent<Image>().sprite = ButtonLock;
            Stars.GetComponent<Image>().enabled = false;
            text.GetComponent<Text>().text = "";
        }
        else
        {
            Stars.GetComponent<Image>().sprite = starSprite[starObj.starCollected];
        }

        
    }

}
