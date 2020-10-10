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

    private void Start()
    {
        // Finds player stats so we know how much damage to do
        playerStats = GetComponentInParent<Character>();
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
}
