using System;
using System.Net;
using UnityEngine;

namespace UnityWebSockets
{
    [RequireComponent(typeof(WebsocketDispatcher))]
    public class WebsocketFactory : MonoBehaviour
    {
        private WebsocketDispatcher dispatcher;

        private void Awake()
        {
            GetDependencies();

            if (dispatcher == null) 
                throw new NullReferenceException("dispatcher is null");
        }

        public WebsocketWrapper CreateWebSocket(IPAddress address, int port)
        {
            GetDependencies();
            string websocketAddress = $"ws://{address}:{port}/";
            return new WebsocketWrapper(dispatcher, websocketAddress);
        }

        public WebsocketWrapper CreateWebSocket(string url)
        {
            GetDependencies();
            return new WebsocketWrapper(dispatcher, url);
        }

        public void GetDependencies()
        {
            if (dispatcher == null)
            {
                dispatcher = GetComponent<WebsocketDispatcher>();
            }
        }
    }
}
