using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class VoiceManager : MonoBehaviour
    {
        public static VoiceManager Instance;
        public string VoiceCommand;

        public bool ActiveVoice;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
                Destroy(this);
        }

        private void Update()
        {
            if (ActiveVoice)
            {
                // Accept Voice commands
                FlowManager.Instance.FinishSequence(VoiceCommand);
            }
        }
    }
}