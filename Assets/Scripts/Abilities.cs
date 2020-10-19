﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public float thrust;
    public float timer;
    public bool dashing;
    public bool dashCooldown;

    //Levles:                     1     2    3
    public float[] dashDamage = { 30f, 50f, 80f };
    public DamageOnHit damageOnHit;

    [SerializeField] PlayerController playerController;
    [SerializeField] private Transform player;


    private void Start()
    {
        thrust = 100.0f;
        timer = 0.1f;
        dashCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.swipe && !dashCooldown)
        {

            StartCoroutine(DashAbility(player));

        }

        if (playerController.hold)
        {
            GroundPound(player);
        }

        if (dashing)
        {
            int dashLevel = UIManager.instance.ButtonLevel(1);
            damageOnHit.DealAbilityDamage(dashDamage[dashLevel]);
        }
    }

    IEnumerator DashAbility(Transform player)
    {
        if (UIManager.instance.ButtonToCheck(1) && player.GetComponent<Player>().CheckForMana(dashDamage[UIManager.instance.ButtonLevel(1)]))
        {
            player.Translate(Vector3.forward * thrust * Time.deltaTime);
            dashing = true;

            yield return new WaitForSeconds(timer);

            player.Translate(Vector3.zero);
            dashing = false;
            playerController.swipe = false;

            StartCoroutine(DashCooldown());
        }
    }

    IEnumerator DashCooldown()
    {
        dashCooldown = true;

        yield return new WaitForSeconds(5);

        dashCooldown = false;

    }

    private void GroundPound(Transform player)
    {

    }
}

