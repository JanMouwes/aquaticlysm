using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartLocalSeaScene()
    {
        SceneManager.LoadScene("LocalSea", LoadSceneMode.Single);
    }
}
