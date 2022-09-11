using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CongratsOnScore : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private int requiredValue = 1000;

    private int playedAmount = 1;

    // Update is called once per frame
    private void Start()
    {
        Gamemanager.butterflyPopped += (sender, args) => PlayRandom();
    }

    private void PlayRandom()
    {
        if (playedAmount * requiredValue < Gamemanager.instance.score)
        {
            SoundManager.instance.PlaySoundEffect(clips[Random.Range(0, clips.Length)]);
            playedAmount++;
        }
    }
}
