using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : MonoBehaviour
{
    public PlayerController player;
    private float damage;
    public List<Character> nearbyEnemies;

    public SphereCollider aoeRange;

    //Levels:                     1   2  3   4    5    6    7    8    9   10
    public int[] enemyHealth = { 30, 40, 56, 76, 100, 132, 176, 236, 316, 420 };

    private void Update()
    {
        transform.position = player.transform.position;

        for (int i = 0; i < nearbyEnemies.Count; i++)
        {
            if (nearbyEnemies[i] == null)
            {
                nearbyEnemies.Remove(nearbyEnemies[i]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null && other.tag == "Enemy")
        {
            nearbyEnemies.Add(other.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nearbyEnemies.Remove(other.GetComponent<Character>());
    }

    public void DealAOEDamage(int playerLevel, int skillButtonLevel)
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

        for (int i = 0; i < nearbyEnemies.Count; i++)
        {
            damage = Mathf.RoundToInt(enemyHealth[playerLevel] / divideBy); ;

            nearbyEnemies[i].TakeDamage(damage);
            nearbyEnemies[i].transform.Translate(Vector3.back * 250f * Time.deltaTime);

            if (nearbyEnemies[i].death)
            {
                player.gameObject.GetComponent<Character>().GainExp(nearbyEnemies[i].expToGive);
                nearbyEnemies.Remove(nearbyEnemies[i]);
                player.chasing = false;
            }
        }
    }
}
