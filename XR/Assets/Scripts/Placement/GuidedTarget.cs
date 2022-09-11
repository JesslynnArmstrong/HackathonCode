using UnityEngine;

public class GuidedTarget : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 target;

    private void Start()
    {
        target = transform.position;
    }

    private void Update()
    {
        if (target == transform.position)
            return;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }
}
