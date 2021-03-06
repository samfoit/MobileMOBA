using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Attached to the player, gives him exp to level him up on the start of the game
/// </summary>
public class Player : Character
{
    private static float lifeTime = 5.0f;
    private float timer = lifeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        GainExp(100);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            RegenHealth();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GainExp(100);
        }

       

    }

    public override void TakeDamage(float damage)
    {

        currentHp -= damage;
        timer = 5f;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHp);
        }
        if (currentHp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void RegenHealth()
    {
        if (currentHp < maxHp)
        {
            currentHp += 1;
        }

        if (currentMp < maxMp)
        {
            currentMp += 1;
        }

        if (currentHp >= maxHp && currentMp >= maxMp)
        {
            timer = 5f;
        }
    }

    public bool CheckForMana(int buttonLevel)
    {
        int manaCost = 0;
        switch (buttonLevel)
        {
            case 1:
                manaCost = Mathf.RoundToInt(maxMp / 3);
                break;
            case 2:
                manaCost = Mathf.RoundToInt(maxMp / 4);
                break;
            case 3:
                manaCost = Mathf.RoundToInt(maxMp / 5);
                break;
            default:
                manaCost = 100;
                Debug.LogError("button level for checkMana is wrong");
                break;
        }

        if (currentMp >= manaCost)
        {
            return true;

        } else
        {
            return false;
        }
    }

    public void TakeMana(int buttonLevel)
    {
        int manaCost = 0;

        switch (buttonLevel)
        {
            case 1:
                manaCost = Mathf.RoundToInt(maxMp / 3);
                break;
            case 2:
                manaCost = Mathf.RoundToInt(maxMp / 4);
                break;
            case 3:
                manaCost = Mathf.RoundToInt(maxMp / 5);
                break;
            default:
                manaCost = 100;
                Debug.LogError("button level for checkMana is wrong");
                break;
        }

        if (currentMp >= manaCost)
        {
            currentMp -= manaCost;
            return;
        }
    }

    
}
