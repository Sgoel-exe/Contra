using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Color low;
    public Color high;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    public void setHealth(int currentHealth, int maxHealth)
    {
        healthBar.gameObject.SetActive(currentHealth < maxHealth);
        healthBar.value = currentHealth;
        healthBar.maxValue = maxHealth;
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthBar.normalizedValue);
    }
}
