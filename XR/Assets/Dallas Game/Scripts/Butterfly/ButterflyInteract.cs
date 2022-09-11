using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyInteract : MonoBehaviour
{
    GestureController controller;

    private void Update()
    {
        if (controller != null && controller.isTrackingGesture)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GestureController temp = other.GetComponentInParent<GestureController>();
        if (temp == null || temp.GetInstanceID() == controller.GetInstanceID()) return;

        controller = temp;

        if (controller.isTrackingGesture)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GestureController temp = other.GetComponentInParent<GestureController>();
        if (temp == null) return;

        controller = null;
    }
}
