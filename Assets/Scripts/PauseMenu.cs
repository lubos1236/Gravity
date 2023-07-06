using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update.
    public bool GameIsPuased = false;
    public GameObject PauseMenuUI;
    GameObject otherObject;
    public Button[] Buttons;
    private bool PauseON = false;

    public GameObject MuteButton;
    public Sprite isMute;
    public Sprite isNotMute;

    private void Start()
    {

        AudioSource music = AudioSource.FindObjectOfType<AudioSource>();
        otherObject = GameObject.FindWithTag("Player");//pozriet

        if (music.mute)
        {
            MuteButton.GetComponent<Image>().sprite = isMute;
        }
        else
        {
            MuteButton.GetComponent<Image>().sprite = isNotMute;
        }
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenuON();
            }
        }
        
    }
    public void Resume()
    {
        GameIsPuased = false;
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);

        Buttons[0].enabled = true;
        Buttons[1].enabled = true;
        Buttons[2].enabled = true;
    }
    private void Pause()
    {
        GameIsPuased = true;
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(true);

        Buttons[0].enabled = false;
        Buttons[1].enabled = false;
        Buttons[2].enabled = false;
    }
    
    
    
    public void PauseMenuON()
    {
        PauseON = !PauseON;
        if(PauseON)
        {
            Pause();
        }
        else
        {
            Resume();
        }

    }

    public void Mute()
    {
        AudioSource music = AudioSource.FindObjectOfType<AudioSource>();
        music.mute = !music.mute;
        if (music.mute)
        {
            MuteButton.GetComponent<Image>().sprite = isMute;
        }
        else
        {
            MuteButton.GetComponent<Image>().sprite = isNotMute;
        }
    }
}


