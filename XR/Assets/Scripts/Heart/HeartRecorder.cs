using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Api;

/// <summary>
/// Recording heart data
/// </summary>
public class HeartRecorder : MonoBehaviour
{
    private List<Api.Event> events = new List<Api.Event>();

    private List<int> data = new List<int>();

    public IReadOnlyCollection<int> Data => data;
    public IReadOnlyCollection<Api.Event> Events => events;

    /// <summary>
    /// Start recording heart data
    /// </summary>
    public void Record()
    {
        Debug.Log("Recording");
        Heart.Instance.HeartRateChanged += OnHeartRateChanged;
    }

    /// <summary>
    /// Stop recording heart data
    /// </summary>
    public void Stop()
    {
        Debug.Log("Stopped");

        Heart.Instance.HeartRateChanged -= OnHeartRateChanged;
    }

    public double GetAverage()
    {
        if (data.Count == 0)
            return 0;

        return data.Average();
    }

    private void OnHeartRateChanged(int heartRate)
    {
        events.Add(new Api.Event("Heartbeat", heartRate));
        data.Add(heartRate);
    }

}
