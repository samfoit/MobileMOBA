using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour

{
    public float thrust = 20.0f;
    public float timer = 1.0f;

    [SerializeField] PlayerController playerController;
    [SerializeField] private Transform player;

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

        yield return new WaitForSeconds(1f);

        player.Translate(Vector3.zero);
        playerController.swipe = false;
    }

    private void GroundPound(Transform player)
    {

    }
}

