using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutlineActivator : MonoBehaviour
{
    private Outline outline;
    public Text levelText;
    public Image healthBar;

    Color outlineColor;

    // Start is called before the first frame update
    void Start()
    {
        outlineColor = new Color(255f, 215f, 0f);
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void ActivateOutline()
    {
        outline.enabled = true;
        levelText.color = outlineColor;
        healthBar.color = outlineColor;
    }

    public void DeactivateOutline()
    {
        outline.enabled = false;
        levelText.color = Color.black;
        healthBar.color = Color.white;
    }
}
