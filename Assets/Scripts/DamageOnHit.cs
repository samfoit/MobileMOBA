using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public PlayerController player;
    private float damage;

    private void Start()
    {
        damage = player.GetComponent<Character>().strength;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            other.GetComponent<Character>().TakeDamage(damage);
        }
    }
}
