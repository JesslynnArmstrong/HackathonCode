using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanged : MonoBehaviour
{
    public string materialName = "butterfly_Albedo(Instance) (UnityEngine.Material)";
    public Material butterfly01;
    public Material butterfly02;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 3) != 1)
        {
            Material chooseMaterial = Random.Range(0, 2) == 1 ? butterfly01 : butterfly02;
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.GetComponent<MeshRenderer>() != null && child.GetComponent<MeshRenderer>().material.name == materialName)
                {
                    child.GetComponent<MeshRenderer>().material = chooseMaterial;

                }
            }
        }
        Destroy(this);
    }
}
