using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to the weapon, responsible for damaging enemies and giving player exp on kill
/// </summary>
public class DamageOnHit : MonoBehaviour
{
    public PlayerController player;
    private float damage;

    private Character playerStats;
    
    public List<Character> enemies;

    //Levels:                     1   2  3   4    5    6    7    8    9   10
    public int[] enemyHealth = { 30, 40, 56, 76, 100, 132, 176, 236, 316, 420 };

    private void Start()
    {
        // Finds player stats so we know how much damage to do
        playerStats = GetComponentInParent<Character>();
    }

    private void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>() != null)
        {
            enemies.Add(other.GetComponent<Character>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        enemies.Remove(other.GetComponent<Character>());
    }

    public void DealDamage()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            damage = playerStats.strength;

            enemies[i].TakeDamage(damage);

            // If the attack kills them, reward the player with exp
            if (enemies[i].death)
            {
                player.gameObject.GetComponent<Character>().GainExp(enemies[i].expToGive);
                enemies.Remove(enemies[i]);
                player.chasing = false;

            }
        }
    }

    public void DealAbilityDamage(int playerLevel, int skillButtonLevel)
    {
        int levelButtonMultiplier = skillButtonLevel;
        float divideBy;

        switch (levelButtonMultiplier)
        {
            case 1:
                divideBy = 5f;
                break;
            case 2:
                divideBy = 4f;
                break;
            case 3:
                divideBy = 3f;
                break;
            default:
                divideBy = 1f;
                Debug.LogError("buttonLevelWrong");
                break;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            damage = Mathf.RoundToInt(enemyHealth[playerLevel] / divideBy);

            enemies[i].TakeDamage(damage);

            // If the attack kills them, reward the player with exp
            if (enemies[i].death)
            {
                player.gameObject.GetComponent<Character>().GainExp(enemies[i].expToGive);
                enemies.Remove(enemies[i]);
                player.chasing = false;
            }
        }
    }

    public void DealGroundPound(int playerLevel, int skillButtonLevel)
    {
        int levelButtonMultiplier = skillButtonLevel;
        float divideBy;

        switch (levelButtonMultiplier)
        {
            case 1:
                divideBy = 5f;
                break;
            case 2:
                divideBy = 4f;
                break;
            case 3:
                divideBy = 3f;
                break;
            default:
                divideBy = 1f;
                Debug.LogError("buttonLevelWrong");
                break;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            damage = Mathf.RoundToInt(enemyHealth[playerLevel] / divideBy);

            enemies[i].TakeDamage(damage);

            enemies[i].transform.Translate(Vector3.back * 500f * Time.deltaTime);

            // If the attack kills them, reward the player with exp
            if (enemies[i].death)
            {
                player.gameObject.GetComponent<Character>().GainExp(enemies[i].expToGive);
                enemies.Remove(enemies[i]);
                player.chasing = false;

            }
        }
    }

}
