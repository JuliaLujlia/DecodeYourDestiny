using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

public class ObjectsScript : MonoBehaviour
{
    public GameObject[] choiceObjects;
    // Skripts (previous scene script and upcoming scene script)
    public TarotCardScript tarotCardScript;
    public HandReadingScript handReadingScript;

    // Video
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    public GameObject videoScreen;

    // Testing Audio Version
    public AudioSource audioSource;

    // Count Course Videos, for EventTrigger: Hand Reading
    private int requiredClicks = 0;
    private int clickedCount = 0;
    private bool[] hasBeenWatched;

    // Start Event after last Video
    private bool allButtonsClicked = false;
    private int lastVideoPlayedIndex = -1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize Watched List
        hasBeenWatched = new bool[choiceObjects.Length];

        foreach (GameObject obj in choiceObjects)
        {
            // Deactivate all Objects
            obj.SetActive(false);
            videoScreen.SetActive(false);
        }

        // Finished Video
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    // Show Objectes based on the previous selected Tarot Card
    public void ShowObjectsBasedOnTarotCard()
    {
        string selectedTarotCard = tarotCardScript.GetSelectedTarotCard();
        // Debug.Log("Selected Tarot Card (ObjectsScript):" + selectedTarotCard);

        // Activate Objects based on Tarot Card
        switch (selectedTarotCard)
        {
            case "Software Engineer":
                ActivateButton(0);
                ActivateButton(1);
                break;

            case "Game Designer":
                ActivateButton(2);
                ActivateButton(3);
                break;

            case "Der Ratlose":
                ActivateButton(0);
                ActivateButton(1);
                ActivateButton(2);
                ActivateButton(3);
                break;

            default:
                Debug.LogWarning("Unknown Tarot Card.");
                break;
        }
    }

    // Helper function
    void ActivateButton(int index)
    {
        choiceObjects[index].SetActive(true);
        requiredClicks++;
    }

    // Start Videos
    public void PlayVideo(int index)
    {
        if (videoClips != null && index >= 0 && index < videoClips.Length)  // Sicherheitsabfrage
        {
            videoScreen.SetActive(true);
            videoPlayer.clip = videoClips[index];
            videoPlayer.Play();
            Debug.Log("Video play: " + videoClips[index].name);


            // Audio
            audioSource.Play();

            // Deactivate all Objects/Colliders
            for (int i = 0; i < choiceObjects.Length; i++)
            {
                Collider objCol = choiceObjects[i].GetComponent<Collider>();
                if (objCol != null)
                {
                    objCol.enabled = false;
                }
            }
            hasBeenWatched[index] = true;
            clickedCount++;

            lastVideoPlayedIndex = index;

            // Event Trigger, all Video Courses watched
            if (clickedCount >= requiredClicks)
            {
                allButtonsClicked = true;
            }
        }
        else
        {
            Debug.LogWarning("Video Index >: " + index);
        }
    }

    // What happens after the Video
    void OnVideoFinished(VideoPlayer videoPlayer) 
    {
        Debug.Log("Video finished.");
        videoScreen.SetActive(false);

        // Reactivate Colliders for not-yet-watched objects
        for (int i = 0; i < choiceObjects.Length; i++)
        {
            if (!hasBeenWatched[i])
            {
                Collider objCol = choiceObjects[i].GetComponent<Collider>();
                if (objCol != null)
                {
                    objCol.enabled = true;
                }
            }
        }


        // allButtonsClicked ist bisschen redudant mit hasBeenWatched, aber egal
        if (allButtonsClicked && videoPlayer.clip == videoClips[lastVideoPlayedIndex]) 
        {
            TriggerAllClickedEvent();
        }
    }


    // Start Hand Reading
    void TriggerAllClickedEvent()
    {
        Debug.Log("You watched all Courses!");

        // Clean desk
        foreach (GameObject obj in choiceObjects)
        {
            obj.SetActive(false);
        }

        videoScreen.SetActive(false);

        // Start new scene
        if (handReadingScript != null)
        {
            handReadingScript.MoveHands();
        }
        else
        {
            Debug.LogWarning("NextPhaseScript not assigned.");
        }
    }
}
