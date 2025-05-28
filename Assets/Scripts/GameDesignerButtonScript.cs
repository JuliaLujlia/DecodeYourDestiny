using UnityEngine;

public class GameDesignerButtonScript : MonoBehaviour
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
