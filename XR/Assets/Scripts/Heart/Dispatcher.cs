using System;
using System.Collections.Generic;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    public static Dispatcher Instance { get; private set; }

    private readonly object tasksLock = new object();
    private readonly Queue<Action> tasks = new Queue<Action>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    /// <summary>
    /// Check if data is in the dispatcher
    /// </summary>
    private void Update()
    {
        lock (tasksLock)
        {
            if (tasks.Count == 0)
                return;

            do
            {
                tasks.Dequeue()?.Invoke();
            }
            while (tasks.Count > 0);
        }
    }

    /// <summary>
    /// Run code on the main thread
    /// </summary>
    /// <param name="callback">The callback you want to add</param>
    public void Invoke(Action callback)
    {
        lock (tasksLock)
        {
            if (callback != null)
            {
                tasks.Enqueue(callback);
            }
        }
    }
}
