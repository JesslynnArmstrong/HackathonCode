using System;
using Flow;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowManager : MonoBehaviour
{
    public static FlowManager Instance;
    
    private SequenceFile _currentSequence;
    
    [SerializeField] private SequenceFile[] sequences;
    [SerializeField] private SequenceInstructor instructor;

    private bool _alreadyStarted;
        
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }

    public void StartFlow()
    {
        if (_alreadyStarted) return;
        
        if (sequences.Length > 0)
        {
            _currentSequence = sequences[0];
            SetNextSequence(_currentSequence);
        }
        else
            Debug.LogError("Sequences and/or transitions is 0!");

        _alreadyStarted = true;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("e"))
            StartFlow();
#endif
    }

    /// <summary>
    /// This method is called when the voice is analyzed. 
    /// </summary>
    /// <param name="transitionMessage"> This is the keyword that has been picked up by the analyzed voice </param>
    public void FinishSequence(string transitionMessage)
    {
        if (transitionMessage == null) throw new ArgumentNullException(nameof(transitionMessage));
        if (transitionMessage == String.Empty) Debug.LogError("transitionMessage can't be empty");
        
        SequenceFile nextSequence = null;
        
        // Loops through the transitions and checks if there is a transition with the same transitionCondition
        // as the transitionMessage.
        foreach (var transition in _currentSequence.transitions)
        {
            if (transition.transitionCondition == transitionMessage)
                nextSequence = sequences[transition.nextSequenceIndex];
        }

        // If there is a transition is met, set the next sequence
        if (nextSequence != null)
        {
            SetNextSequence(nextSequence);
            _currentSequence = nextSequence;
        }
        else
            Debug.LogError("No TransitionCondition is met!");
    }

    /// <summary>
    /// Sets the sequence information to the SequenceInstructor and the correct audio clip to the AudioManagaer
    /// </summary>
    /// <param name="nextSequence"></param>
    private void SetNextSequence(SequenceFile nextSequence)
    {
        if (nextSequence.isRelaxationExercise)
            SceneManager.LoadScene("MVP");

        bool audioOnly = nextSequence.path == string.Empty || nextSequence.noAutoPlay;

        AudioSequenceManager.Instance.SetAudioClip(nextSequence.audioClip, audioOnly);
        instructor.SetNextSequence(nextSequence);
        instructor.LoopAmouth = nextSequence.loopAmount;
    }

    public void NextAutoSequence()
    {
        SequenceFile nextSequence = sequences[_currentSequence.transitions[0].nextSequenceIndex];
        
        SetNextSequence(nextSequence);
        _currentSequence = nextSequence;
    }
}
