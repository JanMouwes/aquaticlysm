using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void InvokeCoroutine(string textObjectName, int seconds)
    {
        GameObject textObject = TextObjectFound(textObjectName);
        // Show a warning by calling the ShowText component
        StartCoroutine(textObject.GetComponent<ShowText>().ShowTextForSeconds(seconds));
    }

    private static GameObject TextObjectFound(string textObjectName)
    {
        return GameObject.Find(textObjectName);
    }
}
