using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManagerScript : MonoBehaviour
{
    public void StartMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
