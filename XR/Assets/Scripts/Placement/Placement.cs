using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Placement : MonoBehaviour
{
    [SerializeField] private GameObject placementGameObject;

    private readonly MLRaycast.QueryParams _raycastParams = new MLRaycast.QueryParams
    {
        Width = 1,
        Height = 1
    };


    // Start is called before the first frame update
    void Start()
    {
        if (Plugin4DSPositionManager.Instance.Position != Vector3.zero)
        {
            placementGameObject.SetActive(true);
            placementGameObject.transform.position = Plugin4DSPositionManager.Instance.Position;
            FlowManager.Instance.StartFlow();
        }
        
        MLInput.Start();
        MLRaycast.Start();
        MLInput.OnControllerButtonDown += CheckRaycast;
    }

    private void CheckRaycast(byte controllerId, MLInput.Controller.Button button)
    {
        _raycastParams.Position = transform.position;
        _raycastParams.Direction = transform.forward;
        _raycastParams.UpVector = transform.up;
        MLRaycast.Raycast(_raycastParams, HandleOnReceiveRaycast);
    }

    private void OnDestroy()
    {
        MLRaycast.Stop();
        MLInput.Stop();
    }

    private void HandleOnReceiveRaycast(MLRaycast.ResultState state, Vector3 point, Vector3 normal, float confidence)
    {
        if (state == MLRaycast.ResultState.HitObserved)
        {
            PlaceObject(point, normal);
        }
    }

    private void PlaceObject(Vector3 point, Vector3 normal)
    {
        placementGameObject.SetActive(true);
        placementGameObject.transform.position = point;
        placementGameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);

        Plugin4DSPositionManager.Instance.Position = point;
        FlowManager.Instance.StartFlow();
    }

}
