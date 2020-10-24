using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attached to all the skill buttons to manage their levels
/// </summary>
public class SkillButton : MonoBehaviour
{
    public int level = 0;
    public Image[] levelCounter;
    public bool isUpgradable = false;
    private UIManager UIManager;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
    }

    public void OnClick()
    {
        if (isUpgradable)
        {
            level++;
            for(int i = 0; i < level; i++)
            {
                levelCounter[i].color = Color.white;
            }
            UIManager.playerStats.levelupCounter--;
            if (UIManager.playerStats.levelupCounter <= 0)
            {
                UIManager.playerStats.levelGain = false;
            }
        }
    }
}
