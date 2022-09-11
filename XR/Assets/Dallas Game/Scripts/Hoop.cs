using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{
    [SerializeField] private AudioSource score;
    public void Score(Vector3 velocity)
    {
        if (velocity.y < 0)
            score.Play();
    }
}
