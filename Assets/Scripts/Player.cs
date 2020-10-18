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

    private void Update()
    {
        // Remember to remove
        if (Input.GetKeyDown(KeyCode.T))
        {
            GainExp(100);
        }
    }
}
