using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    private Slider slider;
    private TMP_Text healthText;

    public static void UpdateHealth(int damage)
    {
        PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") - damage);
    }

    void Start()
    {
        slider = GetComponent<Slider>();
        healthText = GetComponentInChildren<TMP_Text>();
        PlayerPrefs.SetInt("Health", 100);
    }

    void Update()
    {
        slider.value = PlayerPrefs.GetInt("Health");
        healthText.text = $"Health:\n{slider.value}/100";
        
        if (slider.value == 0)
            SceneHandler.GoToScene(2);
    }
}
