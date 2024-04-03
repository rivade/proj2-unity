using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public static void UpdateScore(int amountToAdd)
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + amountToAdd);
    }

    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
    }

    void Update()
    {
        GetComponent<TMP_Text>().text = "Score: " + PlayerPrefs.GetInt("Score");
    }
}
