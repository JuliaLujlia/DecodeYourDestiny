using UnityEngine;

public class HandButtonScript : MonoBehaviour
{
    public HandReadingScript handScript;

    void Start()
    {
        Debug.Log("HandButtonScript");
    }

    void OnMouseDown()
    {
        if (handScript != null)
        {
            handScript.HandDescion(gameObject.name);
            Debug.Log(gameObject.name);
        }
    }
}
