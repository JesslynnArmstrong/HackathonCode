using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flowermesh : MonoBehaviour
{
    SkinnedMeshRenderer[] meshes;

    IEnumerator Start()
    {
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();

        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(2);
            Debug.Log(Mathf.InverseLerp(80, 120, Heart.Instance.HeartRate));
            SetValue(Mathf.InverseLerp(80, 120, Heart.Instance.HeartRate) - 1);
        }
    }

    public void SetValue(float Value)
    {
        foreach (SkinnedMeshRenderer blad in meshes)
        {
            blad.SetBlendShapeWeight(0, Value);
        }

    }
}
