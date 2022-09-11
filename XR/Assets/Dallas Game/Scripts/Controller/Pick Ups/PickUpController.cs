using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class PickUpController : MonoBehaviour
{
    [HideInInspector] public PickUp pickUp;
    [SerializeField] private GameObject pickUpController;

    [HideInInspector] public PickUpState pickUpState = PickUpState.None;
    private Vector3 velocity;
    private Vector3 previousPosition;

    [SerializeField] private GameObject prefab;

    private MLControllerConnectionHandlerBehavior controller;
    [SerializeField] private GestureController gestures;

    private enum PickUpInput
    {
        Button, Trigger, Hand, None
    }
    PickUpInput type;


    private void Start()
    {
        controller = GetComponent<MLControllerConnectionHandlerBehavior>();

        MLInput.OnControllerButtonDown += OnButtonDown;
        MLInput.OnControllerButtonUp += OnButtonUp;
    }

    private void Update()
    {
        if (pickUpState == PickUpState.HasPickUp)
        {
            velocity = (transform.position - previousPosition) / Time.deltaTime;
            if (controller.ConnectedController?.TriggerValue <= .5f)
            {
                OnButtonUp(0, 0);
                type = PickUpInput.Trigger;
            }
            else if (gestures.isTrackingGesture)
            {
                OnButtonUp(0, 0);
                type = PickUpInput.Hand;
            }
        }
        else if (pickUpState == PickUpState.CanPickup)
        {
            if (controller.ConnectedController?.TriggerValue >= .5f && type == PickUpInput.Trigger)
            {
                OnButtonDown(0, 0);
            }
            else if (!gestures.isTrackingGesture && type == PickUpInput.Hand)
            {
                OnButtonDown(0, 0);
            }
        }

        previousPosition = transform.position;
    }

    private void OnButtonDown(byte controller_id, MLInput.Controller.Button button)
    {
        if (pickUpState == PickUpState.CanPickup)
            Attach();
        else
            Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private void OnButtonUp(byte controller_id, MLInput.Controller.Button button)
    {
        if (pickUpState == PickUpState.HasPickUp)
            Detach();
    }


    public void Interact(PickUp pickUp)
    {
        this.pickUp = pickUp;
        pickUpState = PickUpState.CanPickup;
    }

    public void EndInteract()
    {
        if (pickUpState == PickUpState.HasPickUp)
            Detach();
        pickUpState = PickUpState.None;
    }

    private void Attach()
    {
        if (type == PickUpInput.None) type = PickUpInput.Button;
        pickUpState = PickUpState.HasPickUp;
        FixedJoint joint = pickUpController.AddComponent<FixedJoint>();
        joint.connectedBody = pickUp.GetComponent<Rigidbody>();
    }

    private void Detach()
    {
        type = PickUpInput.None;
        Destroy(pickUpController.GetComponent<FixedJoint>());
        if (pickUpState == PickUpState.HasPickUp)
            pickUpState = PickUpState.CanPickup;
        else
            pickUp = null;
        pickUp.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);
    }
}
