using UnityEngine;
using UnityEngine.UI;

public class DateTime : MonoBehaviour
{
    public Text DateTimeText;

    private float _gameTime;
    private string _timeText;
    private int _dayCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        _gameTime = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (dayHasPassed(_gameTime))
        {
            _gameTime = 100.0f;
            _dayCounter++;
        }

        _gameTime -= Time.deltaTime;
        _timeText = _gameTime.ToString();

        DateTimeText.text = "DAY " + _dayCounter;
    }

    private static bool dayHasPassed(float passedGameTime) => passedGameTime <= 0f;
}
