using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Attached to the player, gives him exp to level him up on the start of the game
/// </summary>
public class Player : Character

{
    private static float lifeTime = 5.0f;
    private float timer = lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        GainExp(100);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            RegenHealth();
        }

    }

    public override void TakeDamage(float damage)
    {

        currentHp -= damage;
        timer = 5f;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHp);
        }
        if (currentHp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void RegenHealth()
    {
        if (currentHp != maxHp)
        {
            currentHp += 1;
        }

        if (currentMp != maxMp)
        {
            currentMp += 1;
        }

        if (currentHp == maxHp && currentMp == maxMp)
        {
            timer = 5f;
        }
    }
}
