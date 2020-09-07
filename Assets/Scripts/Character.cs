using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float currentHp = 100f;
    public float maxHp = 100f;

    public float strength = 10f;
    
    public float currentMp = 50f;
    public float maxMp = 50f;

    public float currentExp = 0f;
    public float expToNextLevel = 100f;

    public int level = 1;

    public float expToGive = 100f;

    public bool levelGain = false;

    public bool death = false;

    private void Start()
    {
        currentHp = maxHp;
        currentMp = maxMp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if(currentHp <= 0)
        {
            death = true;
            Destroy(gameObject);
        }
    }

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
