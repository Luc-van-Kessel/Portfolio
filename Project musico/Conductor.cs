using UnityEngine;

public class Conductor : MonoBehaviour
{
    public float songBPM;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    public float firstBeatOffset;

    public AudioSource musicSource;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        secPerBeat = songBPM / 60f;
        dspSongTime = (float)AudioSettings.dspTime;

        musicSource.Play();
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = songPosition / secPerBeat;

        Debug.Log(songPositionInBeats); 
    }
}
