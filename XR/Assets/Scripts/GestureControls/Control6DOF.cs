using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Control6DOF : MonoBehaviour
{
    #region Private Variables
    private MLInput.Controller _controller;
    #endregion

    #region Unity Methods
    private void Start()
    {
        //Start receiving input by the Control
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Right);
    }

    private void OnDestroy()
    {
        //Stop receiving input by the Control
        MLInput.Stop();
    }

    private void Update()
    {
        //Attach the Beam GameObject to the Control
        transform.position = _controller.Position;
        transform.rotation = _controller.Orientation;
    }
    #endregion
}