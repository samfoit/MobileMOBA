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
    // TODO: Change to array
    public float currentHp = 100f;
    public float maxHp = 100f;

    // Damage
    // TODO: Change to array
    public float strength = 10f;

    // Mana
    // TODO: Change to array
    public float currentMp = 50f;
    public float maxMp = 50f;

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

        if(currentExp >= expToNextLevel)
        {
            levelGain = true;
            level++;
            currentExp = 0;
            expToNextLevel *= 2;
        }
    }
}
