using System;
using System.Security.Authentication;
using WebSocketSharp;

namespace UnityWebSockets
{
    public class WebsocketWrapper
    {
        public event EventHandler         OnOpen;
        public event EventHandler         OnClose;
        public event EventHandler<string> OnMessage;

        public bool IsOpen { get; private set;}

        private WebsocketDispatcher dispatcher;
        private WebSocket socket;

        public WebsocketWrapper(WebsocketDispatcher dispatcher, string address)
        {
            if (dispatcher == null || address.IsNullOrEmpty()) throw new ArgumentNullException("arguments cannot be null");

            this.dispatcher = dispatcher;

            socket            = new WebSocket(address);
            //socket.SslConfiguration.EnabledSslProtocols = SslProtocols.Tls12;
            socket.OnOpen    += (sender, e) => this.dispatcher.Enqueue(() => { IsOpen = true;  OnOpen?.Invoke(this, EventArgs.Empty);  });
            socket.OnClose   += (sender, e) => this.dispatcher.Enqueue(() => { IsOpen = false; OnClose?.Invoke(this, EventArgs.Empty); });
            socket.OnMessage += (sender, e) => this.dispatcher.Enqueue(() => {                 OnMessage?.Invoke(this, e.Data);        });
        }

        public void Connect()
        {
            socket.ConnectAsync();
        }

        public void Send(string message)
        {
            if (message == null) throw new ArgumentNullException("message cannot be null");
            if (!IsOpen)         throw new InvalidOperationException("cannot send messages when socket is not open"); 

            socket.SendAsync(message, null);
        }

        public void Stop()
        {
            socket.CloseAsync();
        }
    }
}
