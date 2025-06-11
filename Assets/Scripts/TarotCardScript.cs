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

    private bool collidersActived = false;

    private void Start()
    {
        UnityEngine.Object.FindFirstObjectByType<AudioGuideScript>().playIntro = true;

        foreach (GameObject card in TarotCards)
        {
            card.GetComponent<Collider>().enabled = false;
        }
    }
    void Update()
    {
        // Wait till Audio Guide is finished
        if (!collidersActived)
        {
            AudioGuideScript guide = FindObjectOfType<AudioGuideScript>();
            if (guide != null && guide.firstAudioFinished)
            {
                EnableChoiceObjectColliders();
                collidersActived = true;
            }
        }
    }

    void EnableChoiceObjectColliders()
    {
        foreach (GameObject card in TarotCards)
        {
            if (card.activeSelf)
            {
                Collider col = card.GetComponent<Collider>();
                if (col != null)
                    col.enabled = true;
            }
        }
        Debug.Log("Collider activated after Audio.");
    }

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
            UnityEngine.Object.FindFirstObjectByType<AudioGuideScript>().playAfterCardSelected = true;
        }
    }

    // Return the selected Card (könnte für weitere Funktionen relevant sein, falls nicht. löschen)
    public string GetSelectedTarotCard()
    {
        return selectedTarotCard;
    }
}
