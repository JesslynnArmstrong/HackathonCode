using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using unity4dv;
using UnityEngine.Playables;

[TrackColor(1,1,1)]
[TrackBindingType(typeof(Plugin4DS))]
[TrackClipType(typeof(Playable4DS))]
public class Track4DS : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        Plugin4DS plugin = go.GetComponent<Plugin4DS>();

        foreach (var clip in GetClips())
        {
            Playable4DS myAsset = clip.asset as Playable4DS;

            if (myAsset)
            {
                myAsset.clipstarttime = clip.start;
                myAsset.clipendtime = clip.end;
                if (myAsset.sequence4DS.lastFrame == -1)
                {
                    myAsset.sequence4DS.firstFrame = plugin.FirstActiveFrame;
                    myAsset.sequence4DS.lastFrame = plugin.LastActiveFrame;
                    //Debug.Log(clip.start + " ; " + clip.end);
                }
            }
        }

        return base.CreateTrackMixer(graph, go, inputCount);
    }
}
