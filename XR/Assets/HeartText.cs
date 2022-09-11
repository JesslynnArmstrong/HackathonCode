using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Text))]
public class HeartText : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        Heart.Instance.HeartRateChanged += SetHeartRate;
    }

    void OnDisable()
    {
        Heart.Instance.HeartRateChanged -= SetHeartRate;
    }

    private void SetHeartRate(int heartRate)
    {
        text.text = heartRate.ToString();
    }
}
