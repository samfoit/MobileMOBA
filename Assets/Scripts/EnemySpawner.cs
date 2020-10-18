using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public bool respawn;
    public float spawnDelay;
    private float currentTime;
    private bool spawning;
    
    public Transform playerLocation;
    public Character player;

    void Start()
    {
        Spawn();
        currentTime = spawnDelay;
    }

    void Update()
    {
        if (spawning)
        {
            currentTime -= Time.deltaTime;
            
            if (currentTime <= 0)
            {
                Spawn();
            }
        }
    }

    public void Respawn()
    {
        spawning = true;
        currentTime = spawnDelay;
    }

    void Spawn()
    {
        GameObject enemySpawned = Instantiate(enemy, transform.position, Quaternion.identity);
        Character stats = enemySpawned.GetComponent<Character>();
        stats.spawner = this;

        EnemyAi enemyai = enemySpawned.GetComponent<EnemyAi>();
        enemyai.playerLocation = playerLocation;
        enemyai.player = player;
        
        spawning = false;
    }
}
