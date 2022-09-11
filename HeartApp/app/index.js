import { HeartRateSensor } from "heart-rate";
import * as document from "document";
import { peerSocket } from "messaging";

const hrmData = document.getElementById("hrm-data");
const button1 = document.getElementById("button-1");


const hrm = new HeartRateSensor({ frequency: 1 });

// Heart Rate System
hrm.addEventListener("reading", () => 
{
  hrmData.text = hrm.heartRate ? hrm.heartRate : 0;
  
  // Send data to companion app
  if (peerSocket.readyState === peerSocket.OPEN) 
  {
      let data = 
      {
          heartBeat: hrm.heartRate  
      }
      peerSocket.send(data);
  }
});

hrm.addEventListener("onerror", () => 
{
  hrmData.text = "-";
});

hrm.start();


peerSocket.onmessage = evt => 
{
  console.log(JSON.stringify(evt));
};

button1.addEventListener("click", (evt) => {
  if (peerSocket.readyState === peerSocket.OPEN) 
  {
    peerSocket.send("Click");
  }
})
