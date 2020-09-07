using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public PlayerController player;
    private float damage;

    private Character playerStats;
    private Character enemyStats;

    private void Start()
    {
        playerStats = GetComponentInParent<Character>();
        damage = playerStats.strength;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            enemyStats = other.GetComponent<Character>();

            enemyStats.TakeDamage(damage);
            if (enemyStats.death)
            {
                player.GetComponent<Character>().GainExp(enemyStats.expToGive);
            }
        }
    }
}
