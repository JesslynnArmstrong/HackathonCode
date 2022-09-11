using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GestureController : MonoBehaviour
{

    [SerializeField] private bool trackingRight, trackingLeft;
    [HideInInspector] public bool isTrackingGesture;
    [HideInInspector] public bool isFistGesture;
    [SerializeField] private AudioClip trackingClip;
    [SerializeField] private float minConfidence = 0.3f;

    [SerializeField, Tooltip("KeyPose to track.")]
    private MLHandTracking.HandKeyPose keyPoseToTrack = MLHandTracking.HandKeyPose.NoPose;
    [SerializeField, Tooltip("KeyPose to track.")]
    private MLHandTracking.HandKeyPose secKeyPoseToTrack = MLHandTracking.HandKeyPose.NoPose;

    public GameObject pointText;
    [SerializeField] private Transform playerCamera;

    // Update is called once per frame
    void Update()
    {
        if (!MLHandTracking.IsStarted)
        {
            return;
        }
        float confidenceLeft = trackingLeft ? GetKeyPoseConfidence(MLHandTracking.Left, false) : 0.0f;
        float confidenceRight = trackingRight ? GetKeyPoseConfidence(MLHandTracking.Right, false) : 0.0f;
        float confidenceValue = Mathf.Max(confidenceLeft, confidenceRight);
        isTrackingGesture = confidenceValue > minConfidence;

        confidenceLeft = trackingLeft ? GetKeyPoseConfidence(MLHandTracking.Left, true) : 0.0f;
        confidenceRight = trackingRight ? GetKeyPoseConfidence(MLHandTracking.Right, true) : 0.0f;
        confidenceValue = Mathf.Max(confidenceLeft, confidenceRight);
        isFistGesture = confidenceValue > minConfidence;
    }

    /// <summary>
    /// Get the confidence value for the hand being tracked.
    /// </summary>
    /// <param name="hand">Hand to check the confidence value on. </param>
    /// <returns></returns>
    private float GetKeyPoseConfidence(MLHandTracking.Hand hand, bool isFist)
    {
        if (hand != null)
        {
            if (isFist && hand.KeyPose == MLHandTracking.HandKeyPose.Fist || !isFist && (hand.KeyPose == keyPoseToTrack || hand.KeyPose == secKeyPoseToTrack))
            {
                return hand.HandKeyPoseConfidence;
            }
        }
        return 0.0f;
    }

    public void onTrackingSound ()
    {
        SoundManager.instance.PlaySoundEffect(trackingClip);
    }
}
