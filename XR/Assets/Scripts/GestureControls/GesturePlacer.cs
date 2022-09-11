using MagicLeap.Core.StarterKit;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GesturePlacer : MonoBehaviour
{
    [SerializeField] private GuidedTarget target;
    [SerializeField] private GameObject actor;
    [SerializeField, Range(0,1f)] private float minConfidence;

    private readonly MLRaycast.QueryParams raycastParams = new MLRaycast.QueryParams
    {
        Width = 1,
        Height = 1
    };

    // Update is called once per frame
    private void Update()
    {
        CheckRaycastHand(MLHandTrackingStarterKit.Left);
        CheckRaycastHand(MLHandTrackingStarterKit.Right);
        CheckPlacement(MLHandTrackingStarterKit.Left);
        CheckPlacement(MLHandTrackingStarterKit.Right);
    }

    private void CheckPlacement(MLHandTracking.Hand hand)
    {
        if (hand.KeyPose != MLHandTracking.HandKeyPose.OpenHand) return;

        target.gameObject.SetActive(false);
        actor.SetActive(true);
        actor.transform.position = target.transform.position;
        actor.transform.rotation = target.transform.rotation;
        Debug.Log("Place actor");
    }

    private void CheckRaycastHand(MLHandTracking.Hand hand)
    {
        if (hand.KeyPose != MLHandTracking.HandKeyPose.Finger /*&& hand.HandKeyPoseConfidence > minConfidence*/) return;

        raycastParams.Position = hand.Index.Tip.Position;
        raycastParams.Direction = (hand.Index.PIP.Position - hand.Index.MCP.Position).normalized;
        raycastParams.UpVector = transform.up;
        MLRaycast.Raycast(raycastParams, HandleOnReceiveRaycast);
        Debug.Log("Started moving target");
    }

    private void HandleOnReceiveRaycast(MLRaycast.ResultState state, Vector3 point, Vector3 normal, float confidence)
    {
        if (state == MLRaycast.ResultState.HitObserved)
        {
            target.gameObject.SetActive(true);
            target.SetTarget(point);
            transform.rotation = transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
            Debug.Log("Moved target");
        }
    }
}
