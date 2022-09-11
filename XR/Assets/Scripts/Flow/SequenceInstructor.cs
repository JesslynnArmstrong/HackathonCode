using System;
using DefaultNamespace;
using Flow;
using unity4dv;
using UnityEngine;
using UnityEngine.Events;

public class SequenceInstructor : MonoBehaviour
{
    [SerializeField] private Plugin4DSHelper plugin4DSHelper;
    public int LoopAmouth;
    private SequenceFile _currentSequence;

    /// <summary>
    /// Resets the current frame to 0 and sets the sequence from the sequence path
    /// </summary>
    /// <param name="sequence"></param>
    /// <param name="audioOnly"></param>
    public void SetNextSequence(SequenceFile sequence)
    {
        VoiceManager.Instance.ActiveVoice = false;
        _currentSequence = sequence;
        
        plugin4DSHelper.Load(sequence.path);
        if (sequence.noAutoPlay) return;
        
        if (sequence.startFrame != 0 || sequence.stopFrame != 0 )
            plugin4DSHelper.PlayFromActiveRange(sequence.startFrame, sequence.stopFrame);
        else
            plugin4DSHelper.Play();
    }

    private void Awake()
    {
        plugin4DSHelper.onFinished.AddListener(CheckForLoop);
    }

    private void CheckForLoop()
    {
        if (_currentSequence.autoNextSequence)
        {
            FlowManager.Instance.NextAutoSequence();
            return;
        }
        
        if (LoopAmouth <= 0)
        {
            VoiceManager.Instance.ActiveVoice = true;
        }
        else
        {
            plugin4DSHelper.GoToFrame(_currentSequence.startFrame);
            plugin4DSHelper.Play();
            LoopAmouth--;
        }
    }
    
    
}
