using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;

    private Animator animator;

    float startTime;
    public const float MAX_START_TIME = 0.35f;

    Character stats;

    private bool attack = false;

    private Vector2 startPos;
    private Vector2 currentPos;

    public bool tap, drag, swipe;

    public bool canMove = true;

    public static int click = 0;
    public int[] attacks;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<Character>();
    }

    void Update()
    {
        CheckInput();

        if (tap && canMove)
        {
            Attack();
            tap = false;
        }


        if (drag && canMove)
        {
            if (Input.GetMouseButton(0))
            {
                transform.eulerAngles = GetInputRotation();
                MovePlayer();
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("isRunning", false);
                drag = false;
            }
        }


        if (attack)
        {
            animator.SetTrigger("attack");
            attack = false;
        }
    }

    private void CheckInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
            startPos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        }
        if(startPos.y <= 0.2)
        {
            canMove = false;
            return;
        }
        if(startPos.y > 0.2)
        {
            canMove = true;
        }
        if (Input.GetMouseButton(0))
        {
            currentPos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            if (Time.time - startTime > MAX_START_TIME || startPos != currentPos)
            {
                drag = true;
                tap = false;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - startTime < MAX_START_TIME)
            {
                tap = true;
                drag = false;
            }
        }
    }

    private void Attack()
    {
        string attack = "attack1";

        if (click == 0)
        {
            animator.SetTrigger(attack);
            click++;
        }
        else if (click > 0 && click != attacks.Length)
        {
            int nextClick = click + 1;
            attack = "attack" + nextClick;
            animator.SetTrigger(attack);
            click++;
        }
        else
        {
            click = 1;
            attack = "attack1";
            animator.SetTrigger(attack);
        }
    }

    public void DealDamage()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Character>().TakeDamage(stats.strength);
                if (hit.collider.gameObject.GetComponent<Character>().death)
                {
                    stats.GainExp(hit.collider.gameObject.GetComponent<Character>().expToGive);
                }
            }
            else
            {
                return;
            }
        }
    }

    private void MovePlayer()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        GetComponent<Animator>().SetBool("isRunning", true);
    }

    private Vector3 GetInputRotation()
    {
        return new Vector3(0, Mathf.Atan2(currentPos.x - startPos.x, currentPos.y - startPos.y) * 180 / Mathf.PI, 0);
    }
}
