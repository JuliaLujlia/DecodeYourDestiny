using UnityEngine;
using UnityEngine.Video;


public class AudioGuideScript : MonoBehaviour
{
    public AudioSource[] audioSources;

    // Flags, um zu kontrollieren, welcher Guide abgespielt werden soll
    public bool playIntro;
    public bool playAfterCardSelected;
    public bool playFinal;

    private bool[] wasPlayed; // Um Dopplungen zu vermeiden

    void Start()
    {
        // Array zum Nachverfolgen, ob ein Guide schon gespielt wurde
        wasPlayed = new bool[audioSources.Length];
    }

    void Update()
    {
        // INTRO GUIDE
        if (playIntro && !wasPlayed[0])
        {
            PlayGuide(0);
            wasPlayed[0] = true;
        }

        // NACH KARTENWAHL
        if (playAfterCardSelected && !wasPlayed[1])
        {
            PlayGuide(1);
            wasPlayed[1] = true;
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
