using System.Collections;
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
    public bool canGP = false;
    private bool gpCooldown;
    public bool gp;

    //Levels:                     1     2    3
    public float[] dashDamage = { 5f, 10f, 15f };
    public DamageOnHit damageOnHit;
    public AOEDamage aoeDamage;

    private bool AOECooldown;
    //Levels:                     1    2    3
    public float[] AOEDamage = { 10f, 20f, 30f };

    [SerializeField] PlayerController playerController;
    [SerializeField] private Transform player;


    private void Start()
    {
        thrust = 400.0f;
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

        if (playerController.hold)
        {

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
        UIManager.instance.ButtonCooldown(1);
        dashCooldown = true;

        yield return new WaitForSeconds(5);

        dashCooldown = false;
    }

    IEnumerator ActivateGroundPound()
    {
        UIManager.instance.EnableButton(2);
        canGP = true;
        yield return new WaitForSeconds(0.5f);
        canGP = false;
        UIManager.instance.DisableButton(2);
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
        if (canGP && !gp)
        {
            GetComponent<Animator>().SetTrigger("groundPound");
            StartCoroutine(GroundPoundCooldown());
        }
    }

    IEnumerator GroundPoundCooldown()
    {
        UIManager.instance.ButtonCooldown(2);
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

    private void AOEAbility()
    {
        if (UIManager.instance.ButtonToCheck(3) && player.GetComponent<Player>().CheckForMana(AOEDamage[UIManager.instance.ButtonLevel(3)]) && !AOECooldown)
        {
            player.GetComponent<Player>().TakeMana(AOEDamage[UIManager.instance.ButtonLevel(1)] / 2);
            aoeDamage.DealAOEDamage(AOEDamage[UIManager.instance.ButtonLevel(3)]);
            StartCoroutine(StartAOECooldown());
        }
    }

    IEnumerator StartAOECooldown()
    {
        UIManager.instance.ButtonCooldown(3);
        AOECooldown = true;

        yield return new WaitForSeconds(10);

        AOECooldown = false;
    }
}

