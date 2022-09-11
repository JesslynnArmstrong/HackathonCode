import * as express from 'express';
import * as https from 'https';
import * as WebSocket from 'ws';
import * as fs from 'fs';

const app = express();
const key = fs.readFileSync('C:\\Users\\jeffr\\Downloads\\Websocket tester\\privateKey.key');
const cert = fs.readFileSync('C:\\Users\\jeffr\\Downloads\\Websocket tester\\certificate.crt');


//initialize a simple http server
const server = https.createServer({key, cert},app);

//initialize the WebSocket server instance
const wss = new WebSocket.Server({ server });

wss.on('connection', (ws: WebSocket) => {
    console.log("SOmeboyd connected");
});

//connection is up, let's add a simple simple event
wss.on('message', (msg: string) => {

        wss.clients.forEach(client => {
                if (client != wss) {
                    client.send(msg);
                }
            });
});

wss.on('error', (err) => {
    console.warn(`Client disconnected - reason: ${err}`);
});

//start our server
server.listen(process.env.PORT || 80, () => {
    console.log(`Server started on port ${server.address().port} :)`);
});