using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Transform player;
    public float movementSpeed;
    public float rotateSpeed;
    public float maxDistance;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = checkDist();
        if (dist >= maxDistance)
        {
           moveEnemy();   
        }
        //Can add an else statement here for the enemy to start attacking
        rotateEnemy();
    }

    public void moveEnemy(){
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    public void rotateEnemy(){
        Vector3 targetDir = player.position - transform.position;
        float step = rotateSpeed * Time.deltaTime;
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newRotation);
    }

    public float checkDist(){
        float dist = Vector3.Distance(player.position, transform.position);
        return dist;
    }
}
