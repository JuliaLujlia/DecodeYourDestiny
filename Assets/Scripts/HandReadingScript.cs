using UnityEngine;
using System.Collections;

public class HandReadingScript : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    public TarotCardScript tarotCardScript;
    public Animator handsAnimator;

    private bool handCollidersActivated = false;
    private bool outroPlayed = false;

    public void PlayOutroAnimation()
    {
        if (!outroPlayed)
        {
            handsAnimator.SetTrigger("PlayOutroAnimation");
            outroPlayed = true;
            Debug.Log("Outro animation triggered.");

            TriggerChooseLeftDelayed();
        }
    }

    public void MoveHands()
    {
        Debug.Log("Hand Reading");

        leftHand.transform.position += new Vector3(0, 0, 0);
        rightHand.transform.position += new Vector3(0, 0, 0);

        // Acctivate Collider
        //leftHand.GetComponent<Collider>().enabled = true;
        //rightHand.GetComponent<Collider>().enabled = true;
    }

    void Update()
    {
        if (!handCollidersActivated)
        {
            AudioGuideScript guide = FindObjectOfType<AudioGuideScript>();
            if (guide != null && guide.thirdAudioFinished)
            {
                leftHand.GetComponent<Collider>().enabled = true;
                rightHand.GetComponent<Collider>().enabled = true;

                handCollidersActivated = true;
                Debug.Log("Hand-Collider activated after Audio.");
            }
        }
    }

    public void HandDescion(string handName)
    {
        Debug.Log("HandDescion Script");

        string selectedTarotCard = tarotCardScript.GetSelectedTarotCard();
        Debug.Log("Hand: " + handName);
        Debug.Log("Tarot card: " + selectedTarotCard);

        if (handName == "LeftHand")
        {
            TriggerChooseLeftDelayed();
        }
        else if (handName == "RightHand")
        {
            handsAnimator.SetTrigger("PlayChooseRight");
        }

        if (handName != null)
        {
            // Der Ratlose
            if (handName == "LeftHand" && selectedTarotCard == "Der Ratlose")
            {
                Debug.Log("Left Hand Ending + Ratloser");
                UnityEngine.Object.FindFirstObjectByType<AudioGuideScript>().playAfterRatloserLeft = true;
            }
            else if (handName == "RightHand" && selectedTarotCard == "Der Ratlose")
            {
                Debug.Log("Right Hand Ending + Ratloser");
                UnityEngine.Object.FindFirstObjectByType<AudioGuideScript>().playAfterRatloserRight = true;
            }

            // Software Engineer
            else if (handName == "LeftHand" && selectedTarotCard == "Software Engineer")
            {
                Debug.Log("Left Hand Ending + Software Engineer");
                UnityEngine.Object.FindFirstObjectByType<AudioGuideScript>().playAfterJobLeft = true;
            }
            else if (handName == "RightHand" && selectedTarotCard == "Software Engineer")
            {
                Debug.Log("Right Hand Ending + Software Engineer");
                UnityEngine.Object.FindFirstObjectByType<AudioGuideScript>().playAfterJobRight = true;
            }

            // Game Designer
            else if (handName == "LeftHand" && selectedTarotCard == "Game Designer")
            {
                Debug.Log("Left Hand Ending + Game Designer");
                UnityEngine.Object.FindFirstObjectByType<AudioGuideScript>().playAfterJobLeft = true;
            }
            else if (handName == "RightHand" && selectedTarotCard == "Game Designer")
            {
                Debug.Log("Right Hand Ending + Game Designer");
                UnityEngine.Object.FindFirstObjectByType<AudioGuideScript>().playAfterJobRight = true;
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
    // LEFT HAND
    public void TriggerChooseLeftDelayed()
    {
        StartCoroutine(PlayChooseLeftAfterDelay(14.5f));
    }

    IEnumerator PlayChooseLeftAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (handsAnimator != null)
        {
            handsAnimator.SetTrigger("PlayChooseLeft");
            Debug.Log("Triggering ChooseLeft animation after delay.");

            TriggerChooseRightDelayed();
        }
        else
        {
            Debug.LogWarning("Animator reference is missing!");
        }
    }

    //RIGHT HAND
    public void TriggerChooseRightDelayed()
    {
        StartCoroutine(PlayChooseRightAfterDelay(6.5f)); // Adjust delay after ChooseLeft
    }

    IEnumerator PlayChooseRightAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (handsAnimator != null)
        {
            handsAnimator.SetTrigger("PlayChooseRight");
            Debug.Log("Triggering ChooseRight animation after delay.");
        }
        else
        {
            Debug.LogWarning("Animator reference is missing!");
        }
    }


}
