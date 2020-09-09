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

    // Start is called before the first frame update
    void Start()
    {
        // Sets sliders to proper value
        hpSlider.value = playerStats.currentHp / playerStats.maxHp;
        mpSlider.value = playerStats.currentMp / playerStats.maxMp;
        expSlider.value = playerStats.currentExp / playerStats.expToNextLevel;
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
        if (playerStats.levelGain)
        {
            // Turns every skill button green and allows you to level them up
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].GetComponent<Image>().color = Color.green;
                skillButtons[i].GetComponent<SkillButton>().isUpgradable = true;
            }
        }
        // If player hasn't leveled up buttons should look normal
        if (!playerStats.levelGain)
        {
            // Turns every skill button back to normal
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].GetComponent<Image>().color = Color.white;
                skillButtons[i].GetComponent<SkillButton>().isUpgradable = false;
            }
        }
    }
}
