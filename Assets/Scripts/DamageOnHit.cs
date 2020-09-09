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
    private Character enemyStats;

    private void Start()
    {
        // Finds player stats so we know how much damage to do
        playerStats = GetComponentInParent<Character>();
        damage = playerStats.strength;
    }

    private void OnTriggerExit(Collider other)
    {
        // Checks if weapon has passed through an enemy
        // If it does, damage them
        if (other.GetComponent<Character>() != null)
        {
            enemyStats = other.GetComponent<Character>();

            enemyStats.TakeDamage(damage);

            // If the attack kills them, reward the player with exp
            if (enemyStats.death)
            {
                player.GetComponent<Character>().GainExp(enemyStats.expToGive);
            }
        }
    }
}
