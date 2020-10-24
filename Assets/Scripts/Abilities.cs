﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public float thrust;
    public float timer;
    public bool dashing;
    public bool dashCooldown;

    // "gp" stands for groud pound
    // Levels:                  1    2    3
    public float[] gpDamage = { 5f, 10f, 15f };
    private bool canGP = false;
    private bool gpCooldown;
    public bool gp;

    //Levels:                     1     2    3
    public float[] dashDamage = { 5f, 10f, 15f };
    public DamageOnHit damageOnHit;

    [SerializeField] PlayerController playerController;
    [SerializeField] private Transform player;


    private void Start()
    {
        thrust = 375.0f;
        timer = 0.1f;
        dashCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.swipe)
        {
            StartCoroutine(DashAbility(player));
        }

        if (canGP && playerController.tap)
        {
            PlayGroundPoundAnimation();
        }
    }

    IEnumerator DashAbility(Transform player)
    {
        if (UIManager.instance.ButtonToCheck(1) && player.GetComponent<Player>().CheckForMana(dashDamage[UIManager.instance.ButtonLevel(1)]) && !dashCooldown)
        {
            player.GetComponent<Player>().TakeMana(dashDamage[UIManager.instance.ButtonLevel(1)] / 2);
            player.Translate(Vector3.forward * thrust * Time.deltaTime);
            int dashLevel = UIManager.instance.ButtonLevel(1);
            damageOnHit.DealAbilityDamage(dashDamage[dashLevel]);
            StartCoroutine(DashCooldown());

            yield return new WaitForSeconds(timer);

            player.Translate(Vector3.zero);
            dashing = false;
            playerController.swipe = false;
        }
    }

    IEnumerator DashCooldown()
    {
        dashCooldown = true;

        yield return new WaitForSeconds(5);

        dashCooldown = false;

    }

    IEnumerator ActivateGroundPound()
    {
        canGP = true;
        yield return new WaitForSeconds(0.5f);
        canGP = false;
    }

    public void CheckGroundPound()
    {
        if (UIManager.instance.ButtonToCheck(2) && !gpCooldown)
        {
            StartCoroutine(ActivateGroundPound());
        }
    }

    private void PlayGroundPoundAnimation()
    {
        if (canGP)
        {
            GetComponent<Animator>().SetTrigger("groundPound");
            StartCoroutine(GroundPoundCooldown());
        }
    }

    IEnumerator GroundPoundCooldown()
    {
        gpCooldown = true;
        yield return new WaitForSeconds(5f);
        gpCooldown = false;
    }

    public void EndOfGroundPoundAnimation()
    {
        gp = false;
    }

    public void StartOfGroundPoundAnimation()
    {
        gp = true;
    }

    private void GroundPound()
    {
        damageOnHit.DealGroundPound(gpDamage[UIManager.instance.ButtonLevel(2)]);
        player.GetComponent<Player>().TakeMana(gpDamage[UIManager.instance.ButtonLevel(2)] / 2);
    }

}

