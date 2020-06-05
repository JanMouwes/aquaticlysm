using UnityEngine;

/// <summary>
/// Singleton to show a warning text on the UI, enables and disables a text object for given amount of time.
/// </summary>
public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Enable a text object for a given amount of time (seconds) on the screen to give feedback to the player.
    /// </summary>
    /// <param name="textObjectName">Name of the text object on the scene</param>
    /// <param name="seconds">Amount of time to show the warning for in seconds.</param>
    public void ShowNotification(string textObjectName, int seconds)
    {
        GameObject textObject = FindGameObject(textObjectName);
        // Show a warning by calling the ShowText component
        StartCoroutine(textObject.GetComponent<ShowText>().ShowTextForSeconds(seconds));
    }

    /// <summary>
    /// Find the given object from the screen by its name.
    /// </summary>
    /// <param name="textObjectName">Name of the searched text object.</param>
    /// <returns>The found game object</returns>
    private static GameObject FindGameObject(string textObjectName)
    {
        return GameObject.Find(textObjectName);
    }
}
