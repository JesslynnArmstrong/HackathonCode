using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterlelie : MonoBehaviour {

    public Component[] Blaadjes;
    public float UpDownValue;
    public bool up;
    public bool down;
    float _velocity;
    public float currenttime = 0f;
    public float timetomove = 2f;

    // Use this for initialization
    void Start () {
       // UpDownValue = 100;
        Blaadjes = GetComponentsInChildren(typeof(SkinnedMeshRenderer));
       
        //_velocity=0.0f;
        

    }
    
    public void blaadjesControll(float Value)
    {
        foreach (Component blad in Blaadjes)
        {
            blad.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, Value);
        }

    }

    public void blaadjesControllOpening()
    {
        foreach (Component blad in Blaadjes)
        {
            blad.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight (0, +100);
        }
    }

   

}
