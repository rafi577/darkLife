using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Slider healthBar;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    public void setMaxHealth(float health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void setHealth(float health)
    {
        healthBar.value = health;
        fill.color = gradient.Evaluate(healthBar.normalizedValue);

    }

}
