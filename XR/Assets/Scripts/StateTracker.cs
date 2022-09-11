using UnityEngine;
using UnityEngine.Events;

public class StateTracker : MonoBehaviour
{
    [SerializeField] private StateHandler handler;
    [SerializeField] private UnityEvent[] eventsPerState;

    // Start is called before the first frame update
    private void Start()
    {
        handler.OnNextState += NextState;

        for (int i = 0; i < handler.state; i++)
        {
            NextState(i);
        }

    }

    private void onDestroy()
    {
        handler.OnNextState -= NextState;
    }

    private void NextState(int state)
    {
        if(state >= eventsPerState.Length)
            return;
        eventsPerState[state]?.Invoke();
    }
}
