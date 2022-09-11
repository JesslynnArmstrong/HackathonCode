using System;
using UnityEngine;
using UnityEngine.Events;

public class StateHandler : MonoBehaviour
{
    public Action<int> OnNextState;
    public UnityEvent OnFinish;
    public UnityEvent OnStart;

    public int states;
    public int state;
    [SerializeField] private Timer timer;

    private void Start()
    {
        NextState();
        OnStart.Invoke();
    }

    public void NextState()
    {

        if (state >= states)
        {
            OnFinish?.Invoke();
            return;
        }
        OnNextState?.Invoke(state);
        timer.StartTimer();
        state++;
    }
}
