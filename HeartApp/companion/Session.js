export class Session
{
      url;
      heartData;
      responseCallback;
  
    constructor(url, responseCallback)
    {
      this.url = url;

      this.heartData = [];
      this.responseCallback = responseCallback;
    }
  
    publish(startDate)
    {
      
      let body = JSON.stringify(
          {
            userId: 0,
            sessionName: "Session " + new Date().toLocaleString('en-US'),
            startDate: startDate,
            endDate: new Date(),
            collectables: [],
            events: this.heartData
          })
      
      fetch(this.url, {
        method: "POST",
        headers: {'Content-Type': 'application/json'}, 
        body: body
      }).then(this.responseCallback);
      console.log("Publish" + body);
    }
  
    addData(heartBeat)
    {
      this.heartData.push({ name: "Heartbeat", timestamp: new Date(), value: heartBeat});
    }
}