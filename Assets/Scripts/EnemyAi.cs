using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private Animator animator;
    public Transform playerLocation;
    public Character player;
    private Character enemyStats;
    public float movementSpeed;
    public float rotateSpeed;
    public float maxDistance;
    private float damage;
    

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyStats = GetComponentInParent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = checkDist();
        if (dist > maxDistance)
        {
           moveEnemy();   
           animator.SetBool("attack", false);
        }
        else {
            animator.SetBool("isRunning", false);
            animator.SetBool("attack", true);
            
        }
        
        rotateEnemy();
    }

    public void moveEnemy(){
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerLocation.position, step);
        GetComponent<Animator>().SetBool("isRunning", true);
    }

    public void rotateEnemy(){
        Vector3 targetDir = playerLocation.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newRotation);
    }

    public float checkDist(){
        float dist = Vector3.Distance(playerLocation.position, transform.position);
        return dist;
    }

    public void enemyDamage(){
        damage = enemyStats.strength;
        player.TakeDamage(damage);
    }
}
