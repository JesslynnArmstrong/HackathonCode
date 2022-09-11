using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class HeartCircle : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
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
            image.material.color = Color.red;
        }
        else
        {
            image.material.color = Color.green;
        }
    }
}
