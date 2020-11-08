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
    public Image cooldownFill;
    private UIManager UIManager;
    public bool shouldFade = false;

    public bool isAwakening;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = FindObjectOfType<UIManager>();
        cooldownFill.fillAmount = 0;
        shouldFade = false;
    }

    private void Update()
    {
        if (shouldFade)
        {
            cooldownFill.fillAmount = Mathf.MoveTowards(cooldownFill.fillAmount, 0, 0.2f * Time.deltaTime);
        }
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
                UIManager.instance.DisableAllButtons();
            }
        }
    }
}
