using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Text>().enabled = false;
    }
    public IEnumerator ShowTextFor5Seconds()
    {
        gameObject.GetComponent<Text>().enabled = true;

        yield return new WaitForSeconds(3);

        gameObject.GetComponent<Text>().enabled = false;
    }
}
