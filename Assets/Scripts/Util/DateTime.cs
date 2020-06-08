﻿using UnityEngine;
using UnityEngine.UI;

public class DateTime : MonoBehaviour
{
    public Text DateTimeText;

    private float _gameTime;
    public static int DayCounter = 1;
    public Material Day;
    public Material Night;

    // Start is called before the first frame update
    void Start()
    {
        _gameTime = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox = IsDay(_gameTime) ? Day : Night;
        
        if (dayHasPassed(_gameTime))
        {
            _gameTime = 100.0f;
            DayCounter++;
        }

        _gameTime -= Time.deltaTime;
        
        DateTimeText.text = "DAY " + DayCounter;
    }

    public static bool IsDay(float passedGameTime) => passedGameTime >= 50f;
    private static bool dayHasPassed(float passedGameTime) => passedGameTime <= 0f;
}
