using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class AudioSequenceManager : MonoBehaviour
{
    public static AudioSequenceManager Instance;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }
    
    /// <summary>
    /// Sets the audio clip and plays it right away
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="audioOnly"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SetAudioClip(AudioClip audioClip, bool waitForFinishPlayingAudio)
    {
        if (audioClip == null) return;

        audioSource.clip = audioClip;
        audioSource.Play();
        
        if (waitForFinishPlayingAudio)
            StartCoroutine(WaitForFinishPlayingAudio());
    }

    private IEnumerator WaitForFinishPlayingAudio()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        
        FlowManager.Instance.NextAutoSequence();
    }
}
