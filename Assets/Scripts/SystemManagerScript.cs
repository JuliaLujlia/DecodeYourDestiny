using UnityEngine;

public class SystemManagerScript : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);  // damit es in MainScreen auch funktioniert
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quit Game");
        }
    }
}
