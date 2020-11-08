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
    public bool canGP = false;
    private bool gpCooldown;
    public bool gp;

    public DamageOnHit damageOnHit;
    public AOEDamage aoeDamage;

    private bool AOECooldown;

    [SerializeField] PlayerController playerController;
    [SerializeField] private Transform player;

    private Animator animator;

    public GameObject aoeCollider;
    private Player playerStats;

    private void Start()
    {
        playerStats = GetComponent<Player>();
        thrust = 600.0f;
        timer = 0.1f;
        dashCooldown = false;
        animator = GetComponent<Animator>();
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
            AOEAbility();
        }
    }

    IEnumerator DashAbility(Transform player)
    {
        if (UIManager.instance.ButtonToCheck(1) && player.GetComponent<Player>().CheckForMana(UIManager.instance.ButtonLevel(1)) && !dashCooldown)
        {
            animator.SetTrigger("dash");
            player.GetComponent<Player>().TakeMana(UIManager.instance.ButtonLevel(1));
            player.Translate(Vector3.forward * thrust * Time.deltaTime);
            damageOnHit.DealAbilityDamage(playerStats.level, UIManager.instance.ButtonLevel(1));
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
        damageOnHit.DealGroundPound(playerStats.level, UIManager.instance.ButtonLevel(2));
        player.GetComponent<Player>().TakeMana(UIManager.instance.ButtonLevel(2));
    }

    private void AOEAbility()
    {
        if (UIManager.instance.ButtonToCheck(3) && player.GetComponent<Player>().CheckForMana(UIManager.instance.ButtonLevel(3)) && !AOECooldown)
        {
            animator.SetTrigger("aoe");
            player.GetComponent<Player>().TakeMana(UIManager.instance.ButtonLevel(3));
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

    public void DealAOEDamage()
    {
        aoeDamage.DealAOEDamage(playerStats.level, UIManager.instance.ButtonLevel(3));
    }

    public void ActivateAOECollider()
    {
        aoeCollider.SetActive(true);
    }

    public void DeActivateAOECollider()
    {
        aoeCollider.SetActive(false);
    }
}

