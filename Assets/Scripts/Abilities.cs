using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public float thrust;
    public float timer;

    [SerializeField] PlayerController playerController;
    [SerializeField] private Transform player;


    private void Start()
    {
        thrust = 100.0f;
        timer = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.swipe)
        {

            StartCoroutine(DashAbility(player));

        }

        if (playerController.hold)
        {
            GroundPound(player);
        }
    }

    IEnumerator DashAbility(Transform player)
    {
        player.Translate(Vector3.forward * thrust * Time.deltaTime);

        yield return new WaitForSeconds(timer);

        player.Translate(Vector3.zero);
        playerController.swipe = false;
    }

    private void GroundPound(Transform player)
    {

    }
}

