using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelRating : MonoBehaviour
{
    public GameObject Rating;
    public Sprite[] starSprite;
    private StarManager StarManager;
    public Text RatingText;

    // Start is called before the first frame update
    void Start()
    {
        StarManager = GameObject.Find("End").GetComponent<StarManager>();
    }

    // Update is called once per frame
    void Update()
    {

        ShowRating();
    }

    void ShowRating()
    {
        RatingText.GetComponent<Text>().text = "Time: "+ Mathf.Floor(StarManager.CurrentTime / 60).ToString("0") + ":" + Mathf.Floor(StarManager.CurrentTime % 60).ToString("00") + ":" + Mathf.Floor(((StarManager.CurrentTime % 60) * 100f) % 100).ToString("00") + " / "+
                                                         Mathf.Floor(StarManager.star.timeReward / 60).ToString("0") + ":" + (StarManager.star.timeReward % 60).ToString("00");


        Rating.GetComponent<Image>().sprite = starSprite[StarManager.CurrentStars];
    }
}
