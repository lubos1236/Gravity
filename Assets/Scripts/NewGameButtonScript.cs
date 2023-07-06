using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameButtonScript : MonoBehaviour
{
    public Sprite ButtonLock;
    public Text NewGameText;
    public GameObject NewGameButton;
    public Stars LastLevelStarObj;
    public Stars[] AllLevelStars;
    private int LastLevelComplated;

    void Start()
    {
        LastLevelComplated = PlayerPrefs.GetInt("LastLevelComplated");

        if (false)//!LastLevelStarObj.LevelUnlocked
        {
            NewGameButton.GetComponent<Image>().sprite = ButtonLock;
            NewGameText.GetComponent<Text>().enabled = false;
        }
    }

    public void NewGame()
    {
        LastLevelComplated = 3;
        PlayerPrefs.SetInt("LastLevelComplated", LastLevelComplated);
        for (int i = 0; i <= AllLevelStars.Length-1; i++)
        {
            AllLevelStars[i].LevelUnlocked = false;
            AllLevelStars[i].starCollected = 0;
            
        }
        AllLevelStars[0].LevelUnlocked = true;// povolenie prv=eho levelu

        SceneManager.LoadScene("Level1");
    }
}
