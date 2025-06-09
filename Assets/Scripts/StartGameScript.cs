using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("MainScreen");
        Object.FindFirstObjectByType<AudioGuideScript>().playAfterCardSelected = true;
    }
}
