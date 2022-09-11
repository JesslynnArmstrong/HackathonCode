using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Controlls : MonoBehaviour
{
    private bool canPickUp;
    private bool hasPickUp;

    private Transform colliderGameObject;

    [SerializeField] private GameObject prefab;

    private void Start()
    {
        MLInput.OnControllerButtonDown += OnButtonDown;
        MLInput.OnControllerButtonUp += OnButtonUp;
    }

    private void OnButtonDown(byte controller_id, MLInput.Controller.Button button)
    {
        if ((button == MLInput.Controller.Button.Bumper) &&  !hasPickUp && canPickUp  && MLInput.IsStarted)
        {
            colliderGameObject.transform.SetParent(transform);
            hasPickUp = true;
        }
    }

    private void OnButtonUp(byte controller_id, MLInput.Controller.Button button)
    {
        if ((button == MLInput.Controller.Button.Bumper) && hasPickUp && MLInput.IsStarted)
        {
            colliderGameObject.transform.SetParent(null);
            hasPickUp = false;
        }
        else if(!hasPickUp && MLInput.IsStarted)
        {
            Instantiate(prefab, Vector3.zero, Quaternion.identity, transform.parent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasPickUp) return;

        canPickUp = true;
        colliderGameObject = other.gameObject.transform;
        other.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasPickUp) return;

        canPickUp = false;
        colliderGameObject = null;
        other.GetComponent<MeshRenderer>().material.color = Color.white;

    }
}
