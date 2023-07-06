using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int LastLevelComplated;

    public Image[] img;
    public Button[] bt;

    void Start()
    {
        LastLevelComplated = PlayerPrefs.GetInt("LastLevelComplated");
        if (LastLevelComplated < 4)
        {
            LastLevelComplated = 3;
            SaveLC();
        }
    }

    public void FadeOut()
    {
        

        StartCoroutine(FadeImage(true, img));
            int x = 0;
            while (x < img.Length)
            {
                bt[x].enabled = false;
                x++;
            }
    }

    void SaveLC()
    {
        PlayerPrefs.SetInt("LastLevelComplated", LastLevelComplated);
    }

    public void Play()
    {
        SceneManager.LoadScene(LastLevelComplated);
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void SelectLevel(int LevelNumber)
    {
        if (LevelNumber <= LastLevelComplated - 2)
        {
            //SceneManager.LoadScene(LevelNumber + 2);      //old
            SceneManager.LoadScene("Level"+LevelNumber.ToString());
        }
        else
        {
            Debug.Log("You need to win previous level");
        }

    }

    IEnumerator FadeImage(bool fadeAway, Image[] img)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                int x = 0;
                while (x < img.Length)
                {
                    img[x].color = new Color(1, 1, 1, i);
                    x++;

                }
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                int x = 0;
                while (x < img.Length)
                {
                    img[x].color = new Color(1, 1, 1, i);
                    x++;
                }

                yield return null;
            }
        }
    }


}
