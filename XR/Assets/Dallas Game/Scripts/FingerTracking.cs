using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTracking : MonoBehaviour
{
    private GestureController gesture;

    private bool isAlreadyTouch = false;
    // Start is called before the first frame update
    void Start()
    {
        gesture = GetComponentInParent<GestureController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        interactive(other);
        checkAlreadyFist(other);
    }

    private void OnTriggerStay(Collider other)
    {
        interactive(other);
        interactiveFist(other);
    }

    private void interactive (Collider other)
    {
        if (other.CompareTag("Butterfly"))
        {
            if (gesture.isActiveAndEnabled && gesture.isTrackingGesture)
            {
                Gamemanager.butterflyPopped(this,null);
                Vector3 pos = other.gameObject.transform.position;
                GameObject point = Instantiate(gesture.pointText, pos, Quaternion.identity) as GameObject;
                point.transform.LookAt(gesture.transform);
                gesture.onTrackingSound();
                Destroy(other.gameObject);
            }
        }
    }

    private void checkAlreadyFist(Collider other)
    {
        if (other.CompareTag("Frog"))
        {
            if (gesture.isActiveAndEnabled && gesture.isFistGesture)
            {
                isAlreadyTouch = true;
            }
        }
    }

    private void interactiveFist(Collider other)
    {
        if (other.CompareTag("Frog"))
        {
            if (!isAlreadyTouch)
            {
                if (gesture.isActiveAndEnabled && gesture.isFistGesture)
                {
                    if (other.transform.root.GetComponent<Frog_AI>() != null)
                    {
                        //other.transform.root.GetComponent<Frog_AI>().enabled = false;
                        //other.transform.root.SetParent(this.transform);

                        Vector3 pos = other.gameObject.transform.position;
                        GameObject point = Instantiate(gesture.pointText, pos, Quaternion.identity) as GameObject;
                        point.GetComponent<Increaser>().score = 300;
                        point.transform.LookAt(gesture.transform);
                        gesture.onTrackingSound();
                        Destroy(other.transform.root);
                        Destroy(other.gameObject);
                    }
                }
                else
                {
                    if (other.transform.root.GetComponent<Frog_AI>() != null)
                    {
                        //After tracking
                    }
                }
            }
            else if (gesture.isActiveAndEnabled && !gesture.isFistGesture)
            {
                isAlreadyTouch = false;
            }
        }
    }
}
