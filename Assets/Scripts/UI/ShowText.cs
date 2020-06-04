using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to show a given text fr 3 second to give feedback to the player.
/// </summary>
public class ShowText : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Text>().enabled = false;
    }

    /// <summary>
    /// Show the text for 3 seconds.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowTextFor5Seconds()
    {
        gameObject.GetComponent<Text>().enabled = true;

        yield return new WaitForSeconds(3);

        gameObject.GetComponent<Text>().enabled = false;
    }
}
