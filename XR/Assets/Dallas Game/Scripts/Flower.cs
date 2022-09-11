using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{

    [SerializeField] Flowermesh flower;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Heart.Instance.HasStarted && Heart.Instance.HeartRate > 100 && !flower.isActiveAndEnabled)
        {
            flower.gameObject.SetActive(true);
        }
        else if (flower.isActiveAndEnabled && Heart.Instance.HeartRate < 80)
        {
            Destroy(flower.gameObject, 5);
            Destroy(this, 5);

        }



    }
}
