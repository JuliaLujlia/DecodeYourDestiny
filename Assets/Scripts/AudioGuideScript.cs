using UnityEngine;
using UnityEngine.Video;


public class AudioGuideScript : MonoBehaviour
{
    public AudioSource[] audioSources;

    // Flags, um zu kontrollieren, welcher Guide abgespielt werden soll
    public bool playIntro;
    public bool playAfterCardSelected;
    public bool playAfterAllObjects;
    public bool playAfterRatloserRight;
    public bool playAfterRatloserLeft;
    public bool playAfterJobRight;
    public bool playAfterJobLeft;

    private bool[] wasPlayed; // Um Dopplungen zu vermeiden

    // Collider help var

    public bool firstAudioFinished;
    private bool waitingForFirstAudio = false;
    public bool secondAudioFinished;
    private bool waitingForSecondAudio = false;
    public bool thirdAudioFinished;
    private bool waitingForThirdAudio = false;
    public bool fourthAudioFinished;
    private bool waitingForFourthAudio = false;
    

    void Start()
    {
        // Track
        wasPlayed = new bool[audioSources.Length];
    }

    void Update()
    {
        // INTRO GUIDE 
        if (playIntro && !wasPlayed[0])
        {
            PlayGuide(0);
            waitingForFirstAudio = true;
            wasPlayed[0] = true;
        }

        if (waitingForFirstAudio && !audioSources[0].isPlaying)
        {
            firstAudioFinished = true;
            waitingForFirstAudio = false;
            Debug.Log("AudioGuide 0 finished.");
        }

        // After Card selected
        if (playAfterCardSelected && !wasPlayed[1])
        {
            PlayGuide(1);
            waitingForSecondAudio = true;
            wasPlayed[1] = true;
        }

        if (waitingForSecondAudio && !audioSources[1].isPlaying)
        {
            secondAudioFinished = true;
            waitingForSecondAudio = false;
            Debug.Log("AudioGuide 1 finished.");
        }

        // After all Course Videos
        if (playAfterAllObjects && !wasPlayed[2])
        {
            PlayGuide(2);
            waitingForThirdAudio = true;
            wasPlayed[2] = true;
        }

        if (waitingForThirdAudio && !audioSources[2].isPlaying)
        {
            thirdAudioFinished = true;
            waitingForThirdAudio = false;
            Debug.Log("AudioGuide 2 finished.");
        }

        // Job
        if (playAfterJobRight && !wasPlayed[3])
        {
            PlayGuide(3);
            wasPlayed[3] = true;
        }

        if (playAfterJobLeft && !wasPlayed[4])
        {
            PlayGuide(4);
            wasPlayed[4] = true;
        }

        // Ratloser
        if (playAfterRatloserRight && !wasPlayed[5])
        {
            PlayGuide(5);
            wasPlayed[5] = true;
        }

        if (playAfterRatloserLeft && !wasPlayed[6])
        {
            PlayGuide(6);
            wasPlayed[6] = true;
        }

    }

    void PlayGuide(int index)
    {
        if (index < 0 || index >= audioSources.Length) return;

        // Stop
        foreach (var a in audioSources)
        {
            a.Stop();
        }

        audioSources[index].Play();
        Debug.Log("AudioGuide " + index + " started.");
    }
}
