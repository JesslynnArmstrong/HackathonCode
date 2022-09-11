import { peerSocket } from "messaging";
import { WebSocketWrapper } from "./WebSocketWrapper";
import { Session } from "./Session"

const wsUri = "wss://fitbit.vr-health.nl/";
const websocket = new WebSocketWrapper(wsUri);

const apiUri = "https://future-of-healthcare-hackathon.herokuapp.com/session/create";
const healthData = new Session(apiUri, (data) => {
  console.log(data);
  
  }
);

let recording = false;
let startDataRecording;


peerSocket.onopen = evt => {
  websocket.connect();
}

peerSocket.onmessage = evt => {
  
  if(evt.data == "Click")
  {
    console.log("CLICK!");
      if(recording)
      {
          healthData.publish(startDataRecording);
          recording = false;
      }
      else
        {
          startDataRecording = new Date();
          recording = true;
        }
  }
  else
  {
    if(recording)
      {
        healthData.addData(evt.data.heartBeat);
      }
      websocket.send(evt.data);
  }
};

