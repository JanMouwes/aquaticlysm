using Resources;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DateTime : MonoBehaviour
{
    public const float DAY_LENGTH = 120;

    public static float GameTime;
    public static int DayCounter = 1;

    private string _uiText;
    private float _increaseAmount;

    public Text dateTimeText;
    public Material day;
    public Material night;

    // Start is called before the first frame update
    void Start()
    {
        GameTime = DAY_LENGTH;
        this._increaseAmount = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox = IsDay(GameTime) ? this.day : this.night;
        this._uiText = IsDay(GameTime) ? "DAY" : "NIGHT";

        if (DayHasPassed(GameTime))
        {
            GameTime = DAY_LENGTH;
            DayCounter++;
        }

        GameTime -= Time.deltaTime;
        this._increaseAmount -= Time.deltaTime;

        this.dateTimeText.text = this._uiText + " " + DayCounter;

        if (this._increaseAmount <= 0)
        {
            ResourceManager.Instance.DecreaseResource("food", 3);
            this._increaseAmount = 3f;
        }
    }

    public static bool IsDay() => IsDay(GameTime);
    public static bool IsDay(float passedGameTime) => passedGameTime >= DAY_LENGTH / 2f;
    private static bool DayHasPassed(float passedGameTime) => passedGameTime <= 0f;
}