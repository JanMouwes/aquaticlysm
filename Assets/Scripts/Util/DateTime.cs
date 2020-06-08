using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DateTime : MonoBehaviour
{
    public Text DateTimeText;

    public static float gameTime;

    private string _uiText;
    private int _dayCounter = 1;
    public Material day;
    public Material night;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox = IsDay(gameTime) ? this.day : this.night;
        this._uiText = IsDay(gameTime) ? "DAY" : "NIGHT";

        if (DayHasPassed(gameTime))
        {
            gameTime = 100.0f;
            _dayCounter++;
        }

        gameTime -= Time.deltaTime;

        DateTimeText.text = this._uiText + " " + _dayCounter;
    }

    public static bool IsDay() => IsDay(gameTime);
    public static bool IsDay(float passedGameTime) => passedGameTime >= 50f;
    private static bool DayHasPassed(float passedGameTime) => passedGameTime <= 0f;
}