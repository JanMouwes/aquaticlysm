using UnityEngine;
using UnityEngine.UI;

public class DateTime : MonoBehaviour
{
    public Text dateTimeText;

    private float _gameTime;
    private string _timeText;
    private int _dayCounter = 1;

    // Start is called before the first frame update
    private void Start()
    {
        _gameTime = 100.0f;
    }

    // Update is called once per frame
    private void Update()
    {

        if (DayHasPassed(_gameTime))
        {
            _gameTime = 100.0f;
            _dayCounter++;
        }

        _gameTime -= Time.deltaTime;
        _timeText = _gameTime.ToString();

        dateTimeText.text = "DAY " + _dayCounter;
    }

    private static bool DayHasPassed(float passedGameTime) => passedGameTime <= 0f;
}

﻿using UnityEngine;
using UnityEngine.UI;

public class DateTime : MonoBehaviour
{
    public Text DateTimeText;

    private float _gameTime;
    private int _dayCounter = 1;
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
            _dayCounter++;
        }

        _gameTime -= Time.deltaTime;
        
        DateTimeText.text = "DAY " + _dayCounter;
    }

    private static bool IsDay(float passedGameTime) => passedGameTime <= 50f;
    private static bool dayHasPassed(float passedGameTime) => passedGameTime <= 0f;
}
