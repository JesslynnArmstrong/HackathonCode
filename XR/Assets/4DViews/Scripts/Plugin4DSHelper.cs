using System;
using System.Collections;
using Flow;
using unity4dv;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Plugin4DS))]
public class Plugin4DSHelper : MonoBehaviour
{
    public UnityEvent onFinished;

    private Plugin4DS plugin4DS;
    private int currentFrame = 0;

    private void Awake()
    {
        plugin4DS = GetComponent<Plugin4DS>();
        plugin4DS.OnLastFrame += () => StartCoroutine(NextFrame(() => onFinished.Invoke()));
    }

    public void Destroy()
    {
        plugin4DS.Uninitialize();
    }

    /// <summary>
    /// Load a new 4ds file
    /// </summary>
    /// <param name="SequenceName">4DS Filename</param>
    public void Load(string sequenceName)
    {
        plugin4DS.Uninitialize();
        plugin4DS.SequenceName = sequenceName + (sequenceName.IndexOf(".4ds") >= 0 ? "" : ".4ds");
        plugin4DS.Initialize(true);
        plugin4DS.GotoFrame(0);
    }

    /// <summary>
    /// Load a new 4ds file and play it
    /// </summary>
    /// <param name="sequenceName">4DS Filename</param>
    public void LoadAndPlay(SequenceFile sequence)
    {
        this.Load(sequence.path);
        this.Play();
    }

    /// <summary>
    /// Play the selected 4ds file
    /// </summary>
    public void Play()
    {
        plugin4DS.Play(true);
        if (currentFrame > 0) currentFrame = 0;
    }

    /// <summary>
    /// Pause playing the 4ds
    /// </summary>
    public void Pause()
    {
        plugin4DS.Stop();
        currentFrame = plugin4DS.CurrentFrame;
    }

    /// <summary>
    /// Continue to play 4ds
    /// </summary>
    public void Resume()
    {
        if (currentFrame > 0) plugin4DS.GotoFrame(currentFrame);
        this.Play();
    }

    /// <summary>
    /// Stop playing the 4ds
    /// </summary>
    public void Stop()
    {
        plugin4DS.Stop();
        plugin4DS.GotoFrame(0);
        if (currentFrame > 0) currentFrame = 0;
    }

    /// <summary>
    /// Play the selected 4ds file again
    /// </summary>
    public void Replay()
    {
        plugin4DS.GotoFrame(0);
        this.Play();
    }

    /// <summary>
    /// Call function in next frame
    /// </summary>
    /// <returns></returns>
    public IEnumerator NextFrame(Action callback)
    {
        yield return null;
        callback.Invoke();
    }
    
    /// <summary>
    /// Plays a 4ds file from the startFrame and ends it at the stopFrame
    /// </summary>
    /// <param name="startFrame"></param>
    /// <param name="stopFrame"></param>
    public void PlayFromActiveRange(int startFrame, int stopFrame)
    {
        plugin4DS.FirstActiveFrame = startFrame;
        plugin4DS.LastActiveFrame = stopFrame;
        currentFrame = startFrame;
        plugin4DS.CurrentFrame = startFrame;
        
        plugin4DS.GotoFrame(startFrame);
        plugin4DS.Play(true);
    }

    public void GoToFrame(int frame)
    {
        plugin4DS.GotoFrame(frame);
    }
}
