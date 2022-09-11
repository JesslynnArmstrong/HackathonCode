using UnityEngine;

public class PickUp : MonoBehaviour
{
    private PickUpController controller;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            controller = other.GetComponentInParent<PickUpController>();
            if (controller.pickUpState == PickUpState.None)
                controller.Interact(this);
        }
        else if (other.CompareTag("Hoop"))
        {
            other.GetComponent<Hoop>().Score(rb.velocity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            if (controller.pickUpState == PickUpState.CanPickup && controller.pickUp.gameObject.GetInstanceID() == gameObject.GetInstanceID())
                controller.EndInteract();
        }
    }
}
