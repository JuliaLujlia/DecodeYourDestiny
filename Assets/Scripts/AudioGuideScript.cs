using UnityEngine;
using UnityEngine.Video;


public class AudioGuideScript : MonoBehaviour
{
    public AudioSource[] audioSources;

    // Flags, um zu kontrollieren, welcher Guide abgespielt werden soll
    public bool playAfterCardSelected;
    public bool playAfterAllObjects;
    public bool playAfterRatloserRight;
    public bool playAfterRatloserLeft;
    public bool playAfterJobRight;
    public bool playAfterJobLeft;

    private bool[] wasPlayed; // Um Dopplungen zu vermeiden

    void Start()
    {
        // Array zum Nachverfolgen, ob ein Guide schon gespielt wurde
        wasPlayed = new bool[audioSources.Length];
    }

    void Update()
    {
        // INTRO GUIDE 
        // automatic 

        // After Card selected
        if (playAfterCardSelected && !wasPlayed[1])
        {
            PlayGuide(1);
            wasPlayed[1] = true;
        }

        // After all Course Videos
        if (playAfterAllObjects && !wasPlayed[2])
        {
            PlayGuide(2);
            wasPlayed[2] = true;
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

        // Stoppt alle anderen
        foreach (var a in audioSources)
        {
            a.Stop();
        }

        audioSources[index].Play();
        Debug.Log("AudioGuide " + index + " gestartet.");
    }
}
