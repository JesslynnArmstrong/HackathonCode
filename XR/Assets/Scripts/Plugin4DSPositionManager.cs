using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plugin4DSPositionManager : MonoBehaviour
{
    public static Plugin4DSPositionManager Instance;
    public Vector3 Position;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("1"))
            SceneManager.LoadScene("Part1");
        if (Input.GetKeyDown("2"))
            SceneManager.LoadScene("MVP");
        if (Input.GetKeyDown("3"))
            SceneManager.LoadScene("Part2");
#endif
    }
}