using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Transform enemy;
    private RectTransform canvasTransform;
    private Character stats;
    public Text levelText;
    private void Start()

    {
        canvasTransform = GetComponent<RectTransform>();
        stats = GetComponentInParent<Character>();
    }

    private void Update()
    {
        canvasTransform.SetPositionAndRotation(new Vector3(enemy.transform.position.x, canvasTransform.position.y,
         canvasTransform.position.z), Quaternion.Euler(-65, 180-enemy.rotation.y, 0));
         levelText.text = stats.level.ToString();
    }

    public void SetHealth(float health)
    {
        slider.value = stats.currentHp / stats.maxHp;
    } 
}
