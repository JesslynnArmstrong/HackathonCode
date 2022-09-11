// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
//
// Copyright (c) 2019 Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Creator Agreement, located
// here: https://id.magicleap.com/creator-terms
//
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using MagicLeap.Core;
using UnityEngine;

namespace MagicLeap
{
    /// <summary>
    /// This class handles the functionality of updating the bounding box
    /// for the planes query params through input. This class also updates
    /// the UI text containing the latest useful info on the planes queries.
    /// </summary>
    [RequireComponent(typeof(MLPlanesBehavior))]
    public class PlaneGenerator : MonoBehaviour
    {
        #region Private Variables
        [SerializeField, Tooltip("Flag specifying if plane extents are bounded.")]
        private bool _bounded = false;

        private MLPlanesBehavior _planesComponent;

        private static readonly Vector3 _boundedExtentsSize = new Vector3(0, 0, 0);
        // Distance close to sensor's maximum recognition distance.
        private static readonly Vector3 _boundlessExtentsSize = new Vector3(0, 0, 0);

        private Camera _camera;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Check editor set variables for null references.
        /// </summary>
        void Awake()
        {

            _planesComponent = GetComponent<MLPlanesBehavior>();
            _camera = Camera.main;
        }

        /// <summary>
        /// Start bounds based on _bounded state.
        /// </summary>
        void Start()
        {
            UpdateBounds();
        }

        /// <summary>
        /// Update position of the planes component to camera position.
        /// Planes query center is based on this position.
        /// </summary>
        void Update()
        {
            _planesComponent.gameObject.transform.position = _camera.transform.position;
        }

        #endregion
        /// <summary>
        /// Update plane query bounds extents based on if the current _bounded status is true(bounded)
        /// or false(boundless).
        /// </summary>
        private void UpdateBounds()
        {
            _planesComponent.transform.localScale = _bounded ? _boundedExtentsSize : _boundlessExtentsSize;
        }
    }
    
}