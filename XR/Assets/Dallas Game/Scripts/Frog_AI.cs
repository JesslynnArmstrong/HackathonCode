using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Frog_AI : MonoBehaviour
{
    public Transform jumpParent;
    public Transform plantsParent;
    public float movingSpeed = 5f;

    private Vector3 targetPosition;
    private float jumpPos = 0;

    private void Start()
    {
        if (plantsParent == null)
            plantsParent = GameObject.Find("PlanesParent").transform;
    }


    // Update is called once per frame
    void Update()
    {
        //Check if you stay already finish
        if (Vector3.Distance(targetPosition, transform.position) < 1.5f)
        {
            getNewTarget();
        }


        //Rotation target
        Vector3 rotationAxis = Vector3.up;

        Vector3 targetRotation = Vector3.ProjectOnPlane(new Vector3(targetPosition.x, 0, targetPosition.z) - transform.position, rotationAxis);
        Quaternion newRotation = Quaternion.LookRotation(targetRotation, rotationAxis);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * (movingSpeed));
        //Position target
        transform.position += transform.forward * movingSpeed * Time.deltaTime;

        //Jump effect
        float y = Mathf.Sin(Mathf.Deg2Rad * jumpPos) * 3;
        jumpParent.localPosition = new Vector3(0, y, 0);

        float smoothPoint = jumpPos > 90 ? -jumpPos + 175 : jumpPos - 5;

        float x = (Mathf.Sin(Mathf.Deg2Rad * (jumpPos - 90) + (smoothPoint < 0 ? Mathf.Deg2Rad * (smoothPoint * 8) : 0)) * 30);
        jumpParent.localEulerAngles = new Vector3( x, 0, 0);

        jumpPos += Time.deltaTime * 100;
        if (jumpPos >= 180)
        {
            jumpPos = 0;
        }

    }

    private void getNewTarget()
    {
        Transform[] availableTarget = plantsParent.GetComponentsInChildren<Transform>();

        if (availableTarget.Length > 1)
        {
            targetPosition = availableTarget[Random.Range(1, availableTarget.Length)].position;
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, targetPosition.y, this.gameObject.transform.position.z);
        } else
        {
            targetPosition = new Vector3(Random.Range(-11, 11), 0, Random.Range(-11, 11));
        }
    }
}
