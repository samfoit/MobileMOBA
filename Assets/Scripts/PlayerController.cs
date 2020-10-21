using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves and animates the player based off user input
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed = 3f;

    private Animator animator;

    float startTime;
    public const float MAX_START_TIME = 0.35f;

    private bool attack = false;

    private Vector2 startPos;
    private Vector2 currentPos;

    public bool tap, drag, swipe, hold;

    public static int click = 0;
    public int[] attacks;
    private bool isAttacking = false;

    public bool chasing = false;

    private Abilities abilities;

    [SerializeField] private GameObject Player;
    public GameObject enemy;
    [SerializeField] private string enemyTag = "Enemy";

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // This function checks whether the input is a swipe, tap, or drag
        CheckInput();

        // If the input was a tap, player attacks
        if (tap)
        {
            CheckAttack();
            tap = false;
        }

        // If the input as a drag, moves the player
        if (drag || swipe)
        {
            if (Input.GetMouseButton(0))
            {
                transform.eulerAngles = GetInputRotation();
                MovePlayer();
            }
        }

        // Animates the attack
        if (attack)
        {
            animator.SetTrigger("attack");
            attack = false;
        }

        if (Input.GetMouseButtonUp(0) || hold)
        {
            drag = false;
            animator.SetBool("isRunning", false);
        }

        
        if (chasing && enemy != null)
        {
            AttackPhase(Player.transform.position, enemy.transform.position);
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
            drag = false;
            swipe = false;
            hold = false;
            return;
        }
        if (Input.GetMouseButton(0))
        {
            currentPos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            if (Time.time - startTime > MAX_START_TIME || startPos != currentPos)
            {
                if (startPos == currentPos)
                {
                    hold = true;
                    drag = false;
                    tap = false;
                    swipe = false;
                }
                else
                {
                    drag = true;
                    tap = false;
                    swipe = false;
                    hold = false;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - startTime < 0.7 && currentPos != startPos && Vector2.Distance(startPos, currentPos) >= 0.15)
            {
                swipe = true;
                tap = false;
                drag = false;
                hold = false;
            }
            else if (Time.time - startTime < MAX_START_TIME && currentPos == startPos)
            {
                tap = true;
                drag = false;
                swipe = false;
                hold = false;
            }
        }
    }

    private void CheckAttack()
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
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

    
    public void ActivateAttackPhase()
    {
        chasing = true;
    }
    
    public void AttackPhase(Vector3 Player, Vector3 enemyPosition)
    {
        float distance = Vector3.Distance(Player, enemyPosition);
        transform.LookAt(enemyPosition);

        if (distance > 5.0f)
        {
            MoveTowardsEnemy(Player, enemyPosition);
        }
        else
        {
            animator.SetBool("isRunning", false);
            CheckAttack();
        }


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.tag != enemyTag)
                    {
                        animator.SetBool("isRunning", false);
                        chasing = false;
                        enemy.GetComponent<OutlineActivator>().DeactivateOutline();
                    }
                }
            }
        }
    }

    private void MoveTowardsEnemy(Vector3 Player, Vector3 Enemy)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(Player, Enemy, step);
        transform.eulerAngles = Vector3.RotateTowards(Player, Enemy, 360f, 1f);
        GetComponent<Animator>().SetBool("isRunning", true);
        transform.LookAt(Enemy);
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
