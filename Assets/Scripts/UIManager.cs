using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates any UI components that change for instance the players health and mana bar
/// </summary>
public class UIManager : MonoBehaviour
{
    // Reference to player's ui sliders
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;

    public Character playerStats;

    public GameObject[] skillButtons;

    public GameObject[] levelUpAni;

    public Animator[] animator;

    public static UIManager instance;

    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        // Sets sliders to proper value
        hpSlider.value = playerStats.currentHp / playerStats.maxHp;
        mpSlider.value = playerStats.currentMp / playerStats.maxMp;
        expSlider.value = playerStats.currentExp / playerStats.expToNextLevel;
        DisableAllButtons();
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the values based off player's stats each frame
        hpSlider.value = playerStats.currentHp / playerStats.maxHp;
        mpSlider.value = playerStats.currentMp / playerStats.maxMp;
        expSlider.value = playerStats.currentExp / playerStats.expToNextLevel;

        OnLevelGain();
    }

    /// <summary>
    /// Checks if player level's up each frame and updates skill button UI
    /// </summary>
    private void OnLevelGain()
    {
        // Checks if level gain bool is true on player stats
        if (playerStats.levelupCounter > 0)
        {
            
            // Turns every skill button green and allows you to level them up
            for (int i = 0; i < skillButtons.Length; i++)
            {
                EnableAllButtons();
                levelUpAni[i].SetActive(true);
                animator[i].SetBool("levelBool", true);
                skillButtons[i].GetComponent<SkillButton>().isUpgradable = true;
            }
        }
        // If player hasn't leveled up buttons should look normal
        if (!playerStats.levelGain)
        {
            // Turns every skill button back to normal
            for (int i = 0; i < skillButtons.Length; i++)
            {
                levelUpAni[i].SetActive(false);
                animator[i].SetBool("levelBool", false);
                skillButtons[i].GetComponent<SkillButton>().isUpgradable = false;
            }
        }
    }

    public bool ButtonToCheck(int button)
    {
        if (skillButtons[button - 1].GetComponent<SkillButton>().level > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public int ButtonLevel(int button)
    {
        int i = skillButtons[button - 1].GetComponent<SkillButton>().level;
        return i - 1;
    }

    private void EnableAllButtons()
    {
        for (int i = 0; i < skillButtons.Length; i++)
        {
            skillButtons[i].GetComponent<Button>().interactable = true;
        }
    }

    public void DisableAllButtons()
    {
        StartCoroutine(DisableButton());
    }

    IEnumerator DisableButton()
    {
        for (int i = 0; i < skillButtons.Length; i++)
        {
            skillButtons[i].GetComponent<Button>().interactable = false;
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i == 1)
            {
                continue;
            }

            if (skillButtons[i].GetComponent<SkillButton>().level > 0)
            {
                skillButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ButtonCooldown(int button)
    {
        StartCoroutine(StartButtonCooldown(button));
    }

    IEnumerator StartButtonCooldown(int button)
    {
        skillButtons[button - 1].GetComponent<SkillButton>().cooldownFill.fillAmount = 1;
        skillButtons[button - 1].GetComponent<SkillButton>().shouldFade = true;
        yield return new WaitForSeconds(5f);
        skillButtons[button - 1].GetComponent<SkillButton>().shouldFade = false;
        skillButtons[button - 1].GetComponent<SkillButton>().cooldownFill.fillAmount = 0;
    }

    public void EnableButton(int button)
    {
        skillButtons[button - 1].GetComponent<Button>().interactable = true;
    }

    public void DisableButton(int button)
    {
        skillButtons[button - 1].GetComponent<Button>().interactable = false;
    }
}
