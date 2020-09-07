using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;

    public Character playerStats;

    public GameObject[] skillButtons;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider.value = playerStats.currentHp / playerStats.maxHp;
        mpSlider.value = playerStats.currentMp / playerStats.maxMp;
        expSlider.value = playerStats.currentExp / playerStats.expToNextLevel;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = playerStats.currentHp / playerStats.maxHp;
        mpSlider.value = playerStats.currentMp / playerStats.maxMp;
        expSlider.value = playerStats.currentExp / playerStats.expToNextLevel;

        OnLevelGain();
    }

    private void OnLevelGain()
    {
        if (playerStats.levelGain)
        {
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].GetComponent<Image>().color = Color.green;
                skillButtons[i].GetComponent<SkillButton>().isUpgradable = true;
            }
        }
        if (!playerStats.levelGain)
        {
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].GetComponent<Image>().color = Color.white;
                skillButtons[i].GetComponent<SkillButton>().isUpgradable = false;
            }
        }
    }
}
