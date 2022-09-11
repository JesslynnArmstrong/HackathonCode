using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


[Serializable]
public class Playable4DS : PlayableAsset, ITimelineClipAsset
{    
    internal double clipstarttime;
    internal double clipendtime;

    [SerializeField]
    public TimelineBehaviour4DS sequence4DS = new TimelineBehaviour4DS();

    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<TimelineBehaviour4DS>.Create(graph, sequence4DS);
        
    }
}
