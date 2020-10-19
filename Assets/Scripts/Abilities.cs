using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public float thrust;
    public float timer;
    public bool dashing;

    //Levles:                     1     2    3
    public float[] dashDamage = { 30f, 50f, 80f };
    public DamageOnHit damageOnHit;

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
        dashing = true;

        yield return new WaitForSeconds(timer);

        player.Translate(Vector3.zero);
        dashing = false;
        playerController.swipe = false;
    }

    private void GroundPound(Transform player)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (dashing && other.tag == "Enemy")
        {
            damageOnHit.DealAbilityDamage(dashDamage);
        }
    }
}

