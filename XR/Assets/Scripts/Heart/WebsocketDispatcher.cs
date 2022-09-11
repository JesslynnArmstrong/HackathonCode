using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace UnityWebSockets
{
    public class WebsocketDispatcher : MonoBehaviour
    {
        private ConcurrentQueue<System.Action> queue = new ConcurrentQueue<System.Action>();

        public void Enqueue(System.Action action)
        {
            if (action == null) throw new ArgumentNullException("action cannot be null");

            queue.Enqueue(action);
        }

        private void Update()
        {
            System.Action action = null;
            if (queue.TryDequeue(out action)) action.Invoke();
        }
    }
}

