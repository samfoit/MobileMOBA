using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public string playScene;
    public string tutorialScene;

    public void OnStartButtonPress()
    {
        SceneManager.LoadScene(playScene);
    }

    public void OnTutorialButtonPress()
    {
        SceneManager.LoadScene(tutorialScene);
    }
}
