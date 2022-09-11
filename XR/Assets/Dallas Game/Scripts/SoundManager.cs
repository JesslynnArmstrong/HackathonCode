using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource effectSource;
    public AudioSource musicSource;
    public AudioClip[] clips = new AudioClip[3];


    private enum HeartState
    {
        lowest, Neutral, Active
    }
    private HeartState heartState = HeartState.lowest;

    void Awake()
    {
        if (instance != this)
        {
            instance = this;
           // DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        //ChangeCurrentMusic();
    }

    private HeartState GetCurrentState()
    {
        int heartRate = Heart.Instance.HeartRate;
        if (heartRate < 70)
            return HeartState.lowest;
        return heartRate <= 100 ? HeartState.Neutral : HeartState.Active;
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if(effectSource.isPlaying)
            return;
        effectSource.clip = clip;
        effectSource.Play();
    }

    public void ChangeCurrentMusic()
    {
        musicSource.clip = clips[(int)GetCurrentState()];
        musicSource.Play();
    }
}
