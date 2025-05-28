using UnityEngine;

public class DerRatloseButtonScript : MonoBehaviour
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
