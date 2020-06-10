using Resources;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DateTime : MonoBehaviour
{
    public Text DateTimeText;
    public static float gameTime;
    public static int DayCounter = 1;
    public Material day;
    public Material night;

    private string _uiText;
    private float increaseAmount;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 100.0f;
        increaseAmount = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox = IsDay(gameTime) ? this.day : this.night;
        this._uiText = IsDay(gameTime) ? "DAY" : "NIGHT";

        if (DayHasPassed(gameTime))
        {
            gameTime = 100.0f;
            DayCounter++;
        }

        gameTime -= Time.deltaTime;
        increaseAmount -= Time.deltaTime;

        DateTimeText.text = this._uiText + " " + DayCounter;

        if (increaseAmount <= 0)
        { 
            ResourceManager resourceManager = FindObjectOfType<ResourceManager>();
            resourceManager.DecreaseResource("food", 5);
            increaseAmount = 5f;
        }
    }

    public static bool IsDay() => IsDay(gameTime);
    public static bool IsDay(float passedGameTime) => passedGameTime >= 50f;
    private static bool DayHasPassed(float passedGameTime) => passedGameTime <= 0f;
}