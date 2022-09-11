using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Timer that activates after a certain amount of time
/// </summary>
public class Timer : MonoBehaviour
{
    [SerializeField] private float timerInSeconds;
    [SerializeField] private bool startOnEnable;
    [SerializeField] private UnityEvent OnTimerFinished;

    private bool paused;
    private bool active;
    private float activeTimer;

    /// <summary>
    /// Check if we need to start the timer OnEnable
    /// </summary>
    private void OnEnable()
    {
        if (startOnEnable)
        {
            StartTimer();
        }
    }

    /// <summary>
    /// Keep track of the timer
    /// </summary>
    private void Update()
    {
        if (!active || paused)
            return;

        activeTimer -= Time.deltaTime;
        if (activeTimer <= 0)
        {
            active = false;
            OnTimerFinished?.Invoke();
        }
    }

    /// <summary>
    /// Start a timer. After the timer is finished the object will be enabled.
    /// </summary>
    public void StartTimer()
    {
        active = true;
        activeTimer = timerInSeconds;
    }

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }

    /// <summary>
    /// Stop the activated timers
    /// </summary>
    public void StopTimer()
    {
        active = false;
    }
}
