export class WebSocketWrapper
{
    url;
    webSocket;
  
    constructor(url)
    {
      this.url = url;
      this.webSocket = null;
    }
  
    connect()
    {
      console.log(this.url);
      this.webSocket = new WebSocket(this.url);
      this.webSocket.addEventListener("open", this.onOpen.bind(this));
      this.webSocket.addEventListener("close", this.onClosed.bind(this));      
      this.webSocket.addEventListener("message", this.onMessage.bind(this));
      this.webSocket.addEventListener("error", this.onError.bind(this));
    }
  
    send(message)
    {
      this.webSocket.send(JSON.stringify(message));
    }
  
    onOpen(socket)
    {
      console.log("CONNECTED");
    }
  
    onClosed(socket)
    {
      console.log("CONNECTED");
    }
  
    onMessage(evt)
    {
      console.log(`MESSAGE: ${evt.data}`);
    }
  
    onError(evt)
    {
      console.log(evt);
      if(this.webSocket != null)
      {
        console.log("Error closed");
        //this.webSocket.close();
      } 
    }
}