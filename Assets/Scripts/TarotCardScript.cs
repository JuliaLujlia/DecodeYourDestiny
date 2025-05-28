using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TarotCardScript : MonoBehaviour
{
    // public Button[] TarotCardButtons;
    public GameObject[] TarotCards;
    private string selectedTarotCard;

    // Event Trigger
    [System.Serializable]
    public class TarotCardSelectedEvent : UnityEvent<string> { }
    public TarotCardSelectedEvent OnTarotCardSelectedEvent;

    public void OnTarotCardClicked(GameObject clickedCard)
    {
        // Save selected Tarot Card
        selectedTarotCard = clickedCard.name;
        Debug.Log("Selected Tarot Card: " + selectedTarotCard);

        // Deactivate Collider
        foreach (GameObject card in TarotCards)
        {
            Collider cardCol = card.GetComponent<Collider>();
            if (cardCol != null)
            {
                cardCol.enabled = false;
            }
        }

        // Event
        if (OnTarotCardSelectedEvent != null)
        {
            OnTarotCardSelectedEvent.Invoke(selectedTarotCard);
        }
    }

    // Return the selected Card (könnte für weitere Funktionen relevant sein, falls nicht. löschen)
    public string GetSelectedTarotCard()
    {
        return selectedTarotCard;
    }
}
