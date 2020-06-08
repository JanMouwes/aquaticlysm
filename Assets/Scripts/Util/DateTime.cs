using Events;
using Resources;
using UnityEngine;
using UnityEngine.UI;

public class DateTime : MonoBehaviour
{
    public Text DateTimeText;
    public Material Day;
    public Material Night;
    public static int DayCounter = 1;

    private float _gameTime;
    private float _hungerTime;

    // Start is called before the first frame update
    void Start()
    {
        _gameTime = 100.0f;
        _hungerTime = 0f;
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

        _hungerTime += Time.deltaTime;

        if (_hungerTime >= 5f)
        { 
            ResourceManager.Instance.DecreaseResource("food", 5);
            _hungerTime = 0f;
        }
    }

    public static bool IsDay(float passedGameTime) => passedGameTime >= 50f;
    private static bool dayHasPassed(float passedGameTime) => passedGameTime <= 0f;
}
