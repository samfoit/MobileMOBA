using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public string[] tutorial;
    public int currentLine = 0;

    public Text tutorialText;

    private bool isAnimatingText = false;
    private bool outOfText = false;

    public GameObject playButton;
    public GameObject tutorialButton;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateText(currentLine));
        playButton.SetActive(false);
        tutorialButton.SetActive(false);
    }

    private void Update()
    {
        if (isAnimatingText && Input.GetMouseButtonDown(0))
        {
            ShowFullText();
        }
        else if (!isAnimatingText && Input.GetMouseButtonDown(0))
        {
            currentLine++;
            if (currentLine >= tutorial.Length)
            {
                outOfText = true;
                playButton.SetActive(true);
                tutorialButton.SetActive(true);
            }
            StartCoroutine(AnimateText(currentLine));
        }
    }

    IEnumerator AnimateText(int currentLine)
    {
        if (!outOfText)
        {
            tutorialText.text = "";

            foreach (char letter in tutorial[currentLine])
            {
                isAnimatingText = true;
                tutorialText.text += letter;
                yield return new WaitForSeconds(0.1f);
            }

            isAnimatingText = false;
        }
    }

    public void ShowFullText()
    {
        int line = currentLine - 1;
        tutorialText.text = tutorial[currentLine];
        StopAllCoroutines();
        isAnimatingText = false;
    }
}
