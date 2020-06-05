using UnityEngine;

/// <summary>
/// Singleton to show a warning text on the UI, enables and disables a text object for given amount of time.
/// </summary>
public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Enable a text object for a given amount of time (seconds) on the screen to give feedback to the player.
    /// </summary>
    /// <param name="textObjectName">Name of the text object on the scene</param>
    /// <param name="seconds">Amount of time to show the warning for in seconds.</param>
    public void InvokeCoroutine(string textObjectName, int seconds)
    {
        GameObject textObject = TextObjectFound(textObjectName);
        // Show a warning by calling the ShowText component
        StartCoroutine(textObject.GetComponent<ShowText>().ShowTextForSeconds(seconds));
    }

    /// <summary>
    /// Find the given object from the screen by its name.
    /// </summary>
    /// <param name="textObjectName">Name of the searched text object.</param>
    /// <returns></returns>
    private static GameObject TextObjectFound(string textObjectName)
    {
        return GameObject.Find(textObjectName);
    }
}
