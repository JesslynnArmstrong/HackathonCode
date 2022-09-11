using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;
using MagicLeap;
using MagicLeap.Core;

[RequireComponent(typeof(MLPlanesBehavior))]
public class PlaneGrass : MonoBehaviour
{
    [Tooltip("Object prefab to use for plane visualization.")]
    public GameObject[] PlaneVisualPrefabs;

    public GameObject _planesParent;

    // List of all the planes being rendered.
    private List<List<GameObject>> _planeCache = null;
    private List<uint> _planeFlags = null;

    public int sizeOfPlane;

    private void Awake()
    {
        if (_planesParent == null)
            _planesParent = new GameObject();
        _planeCache = new List<List<GameObject>>();
        _planeFlags = new List<uint>();
        GetComponent<MLPlanesBehavior>().OnQueryPlanesResult += HandleOnPlanesUpdate;
    }

    private void DrawPlanes(MLPlanes.Plane[] planes)
    {
        if (planes == null)
            return;

        // Hide the unused plane cache.
        //int index = planes.Length > 0 ? planes.Length - 1 : 0;
        //for (int i = index; i < _planeCache.Count; ++i)
        //{
        //_planeCache[i].SetActive(false);
        //}

        // Update the active planes.
        for (int i = 0; i < planes.Length; ++i)
        {
            if ((planes[i].Flags & (uint)MLPlanesBehavior.SemanticFlags.Floor) != 0)
            {
                int size = sizeOfPlane / 2;

                for (int x = 0; x < sizeOfPlane; x++)
                {
                    for (int y = 0; y < sizeOfPlane; y++)
                    {
                        Vector3 center = planes[i].Center;
                        center = new Vector3(center.x + (x < size ? -x : x), center.y, center.z + (y < size ? -y : y));

                        GameObject visual = Instantiate(PlaneVisualPrefabs[Random.Range(0, PlaneVisualPrefabs.Length)], center, planes[i].Rotation);
                        visual.transform.SetParent(_planesParent.transform);

                    }
                }
            }
        }

        //RefreshAllPlaneMaterials();
    }

    /// <summary>
    /// Sets correct texture to plane based on surface type
    /// </summary>
    /// <param name="renderer">The renderer component</param>
    /// <param name="flags">The flags of the plane containing the surface type</param>
    private void SetRenderTexture(Renderer renderer, uint flags)
    {
        if ((flags & (uint)MLPlanesBehavior.SemanticFlags.Floor) != 0)
        {
            // renderer.sharedMaterial = FloorMaterial;
        }
    }



    /// <summary>
    /// Updates planes and creates new planes based on detected planes.
    ///
    /// This function reuses previously allocated memory to convert all planes
    /// to the new ones by changing their transforms, it allocates new objects
    /// if the current result ammount is bigger than the ones already stored.
    /// </summary>
    /// <param name="p">The planes component</param>
    public void HandleOnPlanesUpdate(MLPlanes.Plane[] planes, MLPlanes.Boundaries[] boundaries)
    {
        DrawPlanes(planes);
    }
}
