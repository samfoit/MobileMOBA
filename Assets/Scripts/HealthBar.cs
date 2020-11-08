using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Animator animator;
    public Slider slider;
    public Transform enemy;
    private RectTransform canvasTransform;
    private Character stats;
    public Text levelText;
    public Text damageText;
    public Text expText;
    public bool isPlayer;


    private void Start()

    {
        canvasTransform = GetComponent<RectTransform>();
        stats = GetComponentInParent<Character>();
        if (isPlayer)
        {
            
        }
        else
        {
            StopPopup();
        }
        

    }

    private void Update()
    {
        if (!isPlayer)
        {
            canvasTransform.SetPositionAndRotation(new Vector3(enemy.transform.position.x, canvasTransform.position.y,
            canvasTransform.position.z), Quaternion.Euler(-65, 180-enemy.rotation.y, 0));
            levelText.text = stats.level.ToString();
        }
        else 
        {
            canvasTransform.SetPositionAndRotation(new Vector3(enemy.transform.position.x, canvasTransform.position.y,
            canvasTransform.position.z), Quaternion.Euler(-65, 180-enemy.rotation.y, 0));
        }
        
    }

    public void SetHealth(float health)
    {
        if (!isPlayer)
        {
            slider.value = stats.currentHp / stats.maxHp;
        }
    } 

    public void DamagePopup(float damage)
    {
        damageText.text = "-" + damage.ToString();
        damageText.enabled = true;
        GetComponent<Animator>().SetBool("DamageText", true);
    }

    public void StopPopup()
    {
        GetComponent<Animator>().SetBool("DamageText", false);
        damageText.enabled = false;
    }

    public void EXPopup(float exp)
    {
        expText.text = "+" + exp.ToString();
        expText.enabled = true;
        GetComponent<Animator>().SetBool("ExpPopup", true);
    }

    public void stopExpPopup()
    {
        GetComponent<Animator>().SetBool("ExpPopup", false);
        expText.enabled = false;
    }

    public bool getIsPlayer()
    {
        return isPlayer;
    }
}
