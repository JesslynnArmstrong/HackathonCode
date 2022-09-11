using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using unity4dv;

[Serializable]
public class TimelineBehaviour4DS : PlayableBehaviour
{
    [SerializeField]
    public int firstFrame = 0;
    [SerializeField]
    public int lastFrame = -1;

    bool onstart = false;

    Plugin4DS Plugin;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Plugin = playerData as Plugin4DS;

        if (onstart)
        {
            if (Plugin == null) return;

            double duration = playable.GetDuration() * Plugin.Framerate;
            double newSpeed=1;

            if (firstFrame < lastFrame && firstFrame >= 0) {
                newSpeed = (lastFrame - firstFrame) / duration;
                //Debug.Log("new speed : " + newSpeed);
            }
            else {
                newSpeed = (Plugin.SequenceNbOfFrames - firstFrame) / duration;
                //Debug.Log("new speed : " + newSpeed);
            }

            Plugin.SpeedRatio = (float)newSpeed;
            Plugin.GotoFrame(firstFrame);
            Plugin.Play(true);

            onstart = false;
        }

        base.ProcessFrame(playable, info, playerData);
    }


    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        onstart = true;
        base.OnBehaviourPlay(playable, info);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (Plugin)
            Plugin.Play(false);
    }


}
