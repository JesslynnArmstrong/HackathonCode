using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeHeart : MonoBehaviour
{
    [SerializeField] private int heartRate;


    private void Update()
    {
        Heart.Instance.HeartRate = heartRate + Mathf.RoundToInt(10 * Mathf.Sin(Time.deltaTime)) - 5;
    }

}
