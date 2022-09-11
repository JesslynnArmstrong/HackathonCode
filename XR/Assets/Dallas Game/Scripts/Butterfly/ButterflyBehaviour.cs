using UnityEngine;
using System.Collections;

public class ButterflyBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        speed = Random.Range(speed /2, speed);

        float random = Random.value;
        if (random > .5)
        {
            Vector3 rotation = transform.GetChild(0).rotation.eulerAngles;
            rotation.y = -180;
            transform.GetChild(0).rotation = Quaternion.Euler(rotation);
            speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(0,
                                       this.transform.position.y,
                                       0);
        transform.LookAt(targetPostition);
        transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);
    }
}
