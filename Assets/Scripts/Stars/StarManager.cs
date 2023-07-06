using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarManager : MonoBehaviour
{
    public Stars star;
    public Stars NextLevelStar;
    private Sectret_Script SecretS;
    public float CurrentTime = 0.0f;
    public Text Timer;
    private bool TimerON = true;
    public int CurrentStars;

    private int LastLevelComplated;
    private bool NewGameSet = false;
    public GameObject LevelRatingUI;
    private Player_Movement_New PMovement;
    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
            PMovement = GameObject.Find("Player").GetComponent<Player_Movement_New>();

        LastLevelComplated = PlayerPrefs.GetInt("LastLevelComplated");
        if (LastLevelComplated < 4)
        {
            LastLevelComplated = 3;
            SaveLC();
        }

        SecretS = GameObject.Find("Secret_Star").GetComponent<Sectret_Script>();
        Timer.text = Mathf.Floor(CurrentTime / 60).ToString("0") + ":" + (CurrentTime % 60).ToString("00.0");
        CurrentStars = star.starCollected;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (NewGameSet == true)
        {
            LastLevelComplated = 3;
            SaveLC();
            NewGameSet = false;
        }

        



        if (TimerON)
        {
            CurrentTime += Time.deltaTime;  
        }
        Timer.text = Mathf.Floor(CurrentTime / 60).ToString("0") + ":" + Mathf.Floor(CurrentTime % 60).ToString("00") + ":" + Mathf.Floor(((CurrentTime % 60) * 100f) % 100).ToString("00");
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (collision.gameObject.tag == "Player")
        {
            GM.FadeOut();

            TimerON = false;
            PMovement.finished = true;

            if (SceneManager.GetActiveScene().buildIndex == LastLevelComplated)
            {
                LastLevelComplated++;
            }
            

            if (!SecretS.SecretColected && star.timeReward <= CurrentTime)
            {
                CurrentStars = 1;
            }
            else if (!SecretS.SecretColected && star.timeReward >= CurrentTime)
            {
                if (CurrentStars <= 1)
                {
                    CurrentStars = 2;
                }
                else if (CurrentStars == 3)
                {
                    CurrentStars = 4;
                }
            }
            else if (star.timeReward <= CurrentTime && SecretS.SecretColected)
            {
                if (CurrentStars <= 1)
                {
                    CurrentStars = 3;
                }
                else if (CurrentStars == 2)
                {
                    CurrentStars = 4;
                }
            }
            else if (star.timeReward >= CurrentTime && SecretS.SecretColected)
            {
                CurrentStars = 4;
            }
            NextLevelStar.LevelUnlocked = true;
            star.starCollected = CurrentStars;
            LevelRatingUI.SetActive(true);

            SaveLC();
            Destroy(gameObject);
        }
    }

    void SaveLC()
    {
        PlayerPrefs.SetInt("LastLevelComplated", LastLevelComplated);
    }

    

}
