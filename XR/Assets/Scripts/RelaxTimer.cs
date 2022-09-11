using UnityEngine;

[RequireComponent(typeof(Timer))]
public class RelaxTimer : MonoBehaviour
{
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
        Heart.Instance.HeartRateChanged += SetHeartRate;
    }

    void OnDisable()
    {
        Heart.Instance.HeartRateChanged -= SetHeartRate;
    }

    private void SetHeartRate(int heartRate)
    {
        double average = Heart.Instance.BaseLine.GetAverage();

        if (heartRate > average)
        {
            timer.Pause();
        }
        else
        {
            timer.Resume();
        }
    }
}
