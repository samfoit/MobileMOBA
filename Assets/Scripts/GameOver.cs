﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnQuitButtonPress()
    {
        Application.Quit();
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("Start");
    }
    
}
