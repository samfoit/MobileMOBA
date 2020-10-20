using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public float thrust;
    public float timer;
    public bool dashing;
    public bool dashCooldown;

    //Levles:                     1     2    3
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

        if (playerController.hold)
        {
            GroundPound(player);
        }
    }

    IEnumerator DashAbility(Transform player)
    {
        if (UIManager.instance.ButtonToCheck(1) && player.GetComponent<Player>().CheckForMana(dashDamage[UIManager.instance.ButtonLevel(1)]) && !dashCooldown)
        {
            Debug.Log("Dashing");
            player.GetComponent<Player>().TakeMana(dashDamage[UIManager.instance.ButtonLevel(1)] / 4);
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

    private void GroundPound(Transform player)
    {

    }
}

