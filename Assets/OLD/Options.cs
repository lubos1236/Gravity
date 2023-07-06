using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer MainMusic;
    public Slider VolumeSlider;
    private bool toZero;
    public GameObject MuteButton;
    public Sprite isMute;
    public Sprite isNotMute;
    
    void Start()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        AudioSource music = AudioSource.FindObjectOfType<AudioSource>();
        if (music.mute)
        {
            MuteButton.GetComponent<Image>().sprite = isMute;
        }
        else
        {
            MuteButton.GetComponent<Image>().sprite = isNotMute;
        }
    }
    private void Update()
    {
        PlayerPrefs.SetFloat("MusicVolume",VolumeSlider.value);
    }
    public void SetVolume(float volume)
    {
        AudioSource music = AudioSource.FindObjectOfType<AudioSource>();
        MainMusic.SetFloat("Volume", volume);
        if (volume <= -20)
        {
            music.mute = true;
            MuteButton.GetComponent<Image>().sprite = isMute;
            toZero = true;
        }
        else
        {
            music.mute = false;
            MuteButton.GetComponent<Image>().sprite = isNotMute;
            toZero = false;
        }
    }

    public void Mute()
    {
        AudioSource music = AudioSource.FindObjectOfType<AudioSource>();
        if (toZero != true)
        {
            music.mute= !music.mute;
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
    
}
