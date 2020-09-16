using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to the player, gives him exp to level him up on the start of the game
/// </summary>
public class Player : Character
{
    // Start is called before the first frame update
    void Start()
    {
        GainExp(100);
    }
<<<<<<< HEAD
=======

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GainExp(100);
        }
    }
>>>>>>> 7e97a8291791d29a98d2f6288eb5486f3014f358
}
