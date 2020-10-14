using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClicker : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private PlayerController playerController;


    void Update()
    {
        //Checks if player is tapping
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.tag == enemyTag)
                    {
                        Debug.Log("Hit an Enemy");
                        playerController.ActivateAttackPhase();
                    }
                }
            }
        }
    }
   
}

    
