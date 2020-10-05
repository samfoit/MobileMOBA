using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to the player, lets you toggle the weapon he's holding on and off to avoid unnecessary collisions
/// </summary>
public class Weapon : MonoBehaviour
{
    public Collider weaponCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void EnableCollider()
    {
        weaponCollider.enabled = true;
    }

    public void DisableCollider()
    {
        weaponCollider.enabled = false;
    }
}
