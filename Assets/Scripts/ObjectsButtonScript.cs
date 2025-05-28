using UnityEngine;

public class ObjectsButtonScript : MonoBehaviour
{
    public ObjectsScript objectsScript;
    private int videoIndex = -1;

    void Start()
    {
        if (objectsScript != null)
        {
            videoIndex = System.Array.IndexOf(objectsScript.choiceObjects, this.gameObject);
        }

        if (videoIndex == -1)
        {
            Debug.LogWarning("Index konnte nicht gefunden werden für: " + gameObject.name);
        }
    }

    void OnMouseDown()
    {
        if (objectsScript != null && videoIndex != -1)
        {
            objectsScript.PlayVideo(videoIndex);
        }
    }
}
