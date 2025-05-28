using UnityEngine;

public class SoftwareEngineerButtonScript : MonoBehaviour
{
    public TarotCardScript manager; 

    private void OnMouseUpAsButton()
    {
        if (manager != null)
        {
            manager.OnTarotCardClicked(gameObject);
        }
    }
}
