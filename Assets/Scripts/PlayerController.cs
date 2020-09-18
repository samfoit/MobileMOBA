using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Moves and animates the player based off user input
/// </summary>
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
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<Character>();
    }

    void Update()
    {
        // This function checks whether the input is a swipe, tap, or drag
        CheckInput();

        // If the input was a tap, player attacks
        if (tap && canMove)
        {
            CheckAttack();
            tap = false;
        }

        // If the input as a drag, moves the player
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

        // Animates the attack
        if (attack)
        {
            animator.SetTrigger("attack");
            attack = false;
        }
    }

    /// <summary>
    /// Checks the position of the player's input and how long they have kept their finger on screen
    /// Returns with the correct input 
    /// </summary>
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

    private void CheckAttack()
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// In order to properly setup attacks, you must fill in the attacks[] array with the total number of attacks the player has
    /// ie if the samurai has a three hit combo, go into the editor, click the player, find the attacks array and put "3" for size
    /// This function loops through a series of attack string triggers !*(attack animations must be named "attack1", "attack2", ect)*!
    /// And creates the comboing effect shown in this game
    /// </summary>
    IEnumerator Attack()
    {
        isAttacking = true;
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

        yield return new WaitForSeconds(0.9f);
        isAttacking = false;
    }

    // Moves the player and animates them in the running animation
    private void MovePlayer()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        GetComponent<Animator>().SetBool("isRunning", true);
    }

    /// <summary>
    /// Takes the user input and gets the angle to rotate the player based off drag
    /// ie if user drags up the angle will be 0 degrees
    /// </summary>
    /// <returns>Angle of swipe</returns>
    private Vector3 GetInputRotation()
    {
        return new Vector3(0, Mathf.Atan2(currentPos.x - startPos.x, currentPos.y - startPos.y) * 180 / Mathf.PI, 0);
    }
}
