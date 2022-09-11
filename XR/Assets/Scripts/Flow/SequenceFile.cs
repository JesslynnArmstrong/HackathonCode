using UnityEngine;

namespace Flow
{
    [System.Serializable]
    public class SequenceFile
    {
        public string path;
        public int startFrame;
        public int stopFrame;
        public SequenceTransition[] transitions;
        public int loopAmount;
        public LoopMode loopMode;
        public AudioClip audioClip;
        public bool autoNextSequence;
        public bool isRelaxationExercise;
        public bool noAutoPlay;
    }
}