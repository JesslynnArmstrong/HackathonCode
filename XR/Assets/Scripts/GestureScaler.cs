using System.Collections;
using System.Collections.Generic;
using MagicLeap.Core.StarterKit;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GestureScaler : MonoBehaviour
{
    [SerializeField] private float speed = .1f;
    [SerializeField, Range(0, 1)] private float minConfidence = .75f;

    private void Start()
    {
        MLHandTrackingStarterKit.Start();
    }

    private void Update()
    {
        if (!CheckHand(MLHandTrackingStarterKit.Left))
        {
            CheckHand(MLHandTrackingStarterKit.Right);
        }
    }

    private bool CheckHand(MLHandTracking.Hand hand)
    {
        if (hand == null || hand.HandKeyPoseConfidence <= minConfidence) return false;

        switch (hand.KeyPose)
        {
            case MLHandTracking.HandKeyPose.C:
                AddToScale(speed);
                Debug.Log("Larger");

                break;
            case MLHandTracking.HandKeyPose.Pinch:
                AddToScale(-speed);
                Debug.Log("Smaller");
                break;
            default:
                return false;
        }
        return true;
    }

    private void AddToScale(float value)
    {
        Vector3 scale = transform.localScale;
        scale.x += value * Time.deltaTime;
        scale.y += value * Time.deltaTime;
        scale.z += value * Time.deltaTime;

        if (scale.x > 0 && scale.y > 0 && scale.z > 0)
        {
            Debug.Log("test   ");
            transform.localScale = scale;
        }

    }
}
