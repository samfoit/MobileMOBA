using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFXPlayer : MonoBehaviour
{
    public ParticleSystem[] swordFX;

    public void PlaySlashFX(int play)
    {
        swordFX[play].Play();
    }
}
