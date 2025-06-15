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
    public AudioSource[] audioSources;

    // Count Course Videos, for EventTrigger: Hand Reading
    private int requiredClicks = 0;
    private int clickedCount = 0;
    private bool[] hasBeenWatched;

    // Start Event after last Video
    private bool allButtonsClicked = false;
    private int lastVideoPlayedIndex = -1;

    private bool objectCollidersActived = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize Watched List
        hasBeenWatched = new bool[choiceObjects.Length];

        foreach (GameObject obj in choiceObjects)
        {
            // Deactivate all Objects
            obj.GetComponent<Collider>().enabled = false;
            obj.SetActive(false);
            videoScreen.SetActive(false);
        }

        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;

        // Finished Video
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        // Wait till Audio Guide is finished
        if (!objectCollidersActived)
        {
            AudioGuideScript guide = FindObjectOfType<AudioGuideScript>();
            if (guide != null && guide.secondAudioFinished)
            {
                EnableChoiceObjectColliders();
                objectCollidersActived = true;
            }

        }
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

    void EnableChoiceObjectColliders()
    {
        foreach (GameObject obj in choiceObjects)
        {
            if (obj.activeSelf)
            {
                Collider col = obj.GetComponent<Collider>();
                if (col != null)
                    col.enabled = true;
            }
        }
        Debug.Log("Object Colliders activated after Audio.");
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
            foreach (var audio in audioSources)
            {
                if (audio.isPlaying)
                    audio.Stop();
            }

            if (index < audioSources.Length && audioSources[index] != null)
            {
                audioSources[index].Play();
            }

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

            // Light deactivate
            Light objLight = choiceObjects[index].GetComponentInChildren<Light>();
            if (objLight != null)
            {
                objLight.enabled = false;
            }

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
        UnityEngine.Object.FindFirstObjectByType<AudioGuideScript>().playAfterAllObjects = true;  // Trigger Audio


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
