using UnityEngine;

public class HandReadingScript : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    public TarotCardScript tarotCardScript;

    public void MoveHands()
    {
        Debug.Log("Hand Reading");

        leftHand.transform.position += new Vector3(0, 0, -0.5f);
        rightHand.transform.position += new Vector3(0, 0, -0.5f);

        // Acctivate Collider
        leftHand.GetComponent<Collider>().enabled = true;
        rightHand.GetComponent<Collider>().enabled = true;
    }

    public void HandDescion(string handName)
    {
        Debug.Log("HandDescion Script");

        string selectedTarotCard = tarotCardScript.GetSelectedTarotCard();

        if (handName != null)
        {
            // Der Ratlose
            if (handName == "Left Hand" && selectedTarotCard == "Der Ratlose")
            {
                Debug.Log("Left Hand Ending + Ratloser");
            }
            else if (handName == "Right Hand" && selectedTarotCard == "Der Ratlose")
            {
                Debug.Log("Right Hand Ending + Ratloser");
            }

            // Software Engineer
            else if (handName == "Left Hand" && selectedTarotCard == "Software Engineer")
            {
                Debug.Log("Left Hand Ending + Software Engineer");
            }
            else if (handName == "Right Hand" && selectedTarotCard == "Software Engineer")
            {
                Debug.Log("Right Hand Ending + Software Engineer");
            }

            // Game Designer
            else if (handName == "Left Hand" && selectedTarotCard == "Game Designer")
            {
                Debug.Log("Left Hand Ending + Game Designer");
            }
            else if (handName == "Right Hand" && selectedTarotCard == "Game Designer")
            {
                Debug.Log("Right Hand Ending + Game Designer");
            }

            else
            {
                Debug.Log("Error, TarotCard unknown.");
            }
            // Deactivate Collider
            leftHand.GetComponent<Collider>().enabled = false;
            rightHand.GetComponent<Collider>().enabled = false;
        }
        else
        {
            Debug.Log("Error, Hand unknown.");
        }
    }
}
