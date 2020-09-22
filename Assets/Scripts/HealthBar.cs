using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Transform enemy;
    private RectTransform canvasTransform;

    private void Start()
    {
        canvasTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        canvasTransform.SetPositionAndRotation(new Vector3(enemy.transform.position.x, canvasTransform.position.y,
         canvasTransform.position.z), Quaternion.Euler(-65, 180-enemy.rotation.y, 0));
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
