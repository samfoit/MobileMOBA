using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all the stats used on each enemy
/// </summary>
public class Character : MonoBehaviour
{
    public HealthBar healthBar;

    // HP
    public float currentHp = 100f;
    //Levels:                     0    1     2     3     4     5     6     7     8     9     10
    public float[] maxHpValues = {0f, 100f, 150f, 200f, 250f, 300f, 350f, 400f, 450f, 500f, 550f};
    public float maxHp;

    // Damage
    //Levels:                        0    1    2    3    4    5    6    7    8    9    10
    public float[] strengthValues = {0f, 10f, 20f, 30f, 40f, 50f, 60f, 70f, 80f, 90f, 100f};
    public float strength;

    // Mana
    public float currentMp = 50f;
    //Levels:                     0    1    2    3     4     5     6     7     8     9     10
    public float[] maxMpValues = {0f, 50f, 75f, 100f, 125f, 150f, 175f, 200f, 225f, 250f, 275f};
    public float maxMp;

    // Exp
    // TODO: Change to array
    public float currentExp = 0f;
    public float expToNextLevel = 100f;

    // Level
    public int level = 0;

    // Amount of exp an enemy gives the player on death
    public float expToGive = 100f;

    // Bools checking if character has died or leveled up (used in other scripts)
    public bool levelGain = false;
    public bool death = false;

    private void Start()
    {
        maxHp = maxHpValues[level];
        strength = strengthValues[level];
        maxMp = maxMpValues[level];
        currentHp = maxHp;
        currentMp = maxMp;

        healthBar.SetMaxHealth(maxHp);
    }

    /// <summary>
    /// Decreases characters health and deletes them if health is less than or equal to 0
    /// </summary>
    /// <param name="damage">Damage amount</param>
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        healthBar.SetHealth(currentHp);
        if(currentHp <= 0)
        {
            death = true;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Grants exp to player
    /// Called in the DamageOnHit script
    /// </summary>
    /// <param name="expToGain">Amount of exp gained</param>
    public void GainExp(float expToGain)
    {
        currentExp += expToGain;

        if(currentExp >= expToNextLevel && level < 10)
        {
            levelGain = true;
            level++;
            currentExp = 0;
            expToNextLevel *= 2;
            LevelUp(levelGain);
        }
    }

    //Wally WIP code to level up
    public void LevelUp(bool levelGain)
    {
        if (levelGain)
        {
            maxHp = maxHpValues[level];
            strength = strengthValues[level];
            maxMp = maxMpValues[level];

            levelGain = false;
        }
    }
}
