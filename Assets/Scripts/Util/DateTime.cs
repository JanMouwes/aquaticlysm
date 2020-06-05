﻿using System.Runtime.Serialization;
 using UnityEngine;
using UnityEngine.UI;

public class DateTime : MonoBehaviour
{
    public Text DateTimeText;

    public static float gameTime;

    private string uiText;
    private int _dayCounter = 1;
    public Material Day;
    public Material Night;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox = IsDay(gameTime) ? Day : Night;
        uiText = IsDay(gameTime) ? "DAY" : "NIGHT";
        
        if (dayHasPassed(gameTime))
        {
            gameTime = 100.0f;
            _dayCounter++;
        }

        gameTime -= Time.deltaTime;
        
        DateTimeText.text = uiText + " " + _dayCounter;
    }
    
    public static bool IsDay() => IsDay(gameTime);
    public static bool IsDay(float passedGameTime) => passedGameTime >= 50f;
    private static bool dayHasPassed(float passedGameTime) => passedGameTime <= 0f;
}
