using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Image fillImage;
    [SerializeField] Slider slider;
    [SerializeField] Manager manager;



    [Header("Health Stats")]
    [SerializeField]float MaxHealth;
    public float currentHealth;



    void Start()
    {
        
    }


    void Update()
    {
        HealthBar();
        Lost();
    }


    void HealthBar()
    {
        if(slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if(slider.value > slider.minValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        float fillValue = currentHealth / MaxHealth;
        slider.value = fillValue;
    }

    void Lost()
    {
        if(currentHealth <= 0)
        {
            manager.Lost();
        }
        if(currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
    }


}
