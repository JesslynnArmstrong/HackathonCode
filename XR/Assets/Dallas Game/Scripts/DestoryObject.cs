using UnityEngine;

public class DestoryObject : MonoBehaviour
{
    public float timeSec;


    void Start()
    {
        Destroy(this.gameObject, timeSec);
    }
}
