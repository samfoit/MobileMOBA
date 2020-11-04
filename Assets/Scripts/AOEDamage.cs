using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : MonoBehaviour
{
    public PlayerController player;
    private float damage;

    private Character playerStats;
    public List<Character> nearbyEnemies;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            nearbyEnemies.Add(other.GetComponent<Character>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        nearbyEnemies.Remove(other.GetComponent<Character>());
    }

    public void DealAOEDamage(float AOEDamage)
    {
        for (int i = 0; i < nearbyEnemies.Count; i++)
        {
            damage = AOEDamage;

            nearbyEnemies[i].TakeDamage(damage);

            if (nearbyEnemies[i].death)
            {
                player.gameObject.GetComponent<Character>().GainExp(nearbyEnemies[i].expToGive);
                nearbyEnemies.Remove(nearbyEnemies[i]);
                player.chasing = false;

            }
        }
    }
}
