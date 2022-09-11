using System;
using UnityEngine;
using UnityEngine.Events;
using UnityWebSockets;

[RequireComponent(typeof(WebsocketFactory))]
[RequireComponent(typeof(Dispatcher))]
public class Heart : MonoBehaviour
{
    private class HeartData
    {
        public int heartBeat;

        public HeartData(int heartBeat)
        {
            this.heartBeat = heartBeat;
        }
    }

    public static Heart Instance { get; private set; }
    public bool HasStarted { get; private set; }

    [SerializeField] private int _heartRate;
    private readonly object lockGuard = new object();

    public Action OnHeartRateSensorConnected;
    public Action<int> HeartRateChanged;

    [SerializeField] private UnityEvent OnHeartRateSensor;

    public HeartRecorder BaseLine;
    private WebsocketWrapper socket;

    public int HeartRate
    {
        get
        {
            lock (lockGuard)
            {
                return _heartRate;
            }
        }
        set
        {
            lock (lockGuard)
            {
                _heartRate = value;
                Dispatcher.Instance.Invoke(() => HeartRateChanged?.Invoke(value));

                if (HasStarted)
                    return;

                HasStarted = true;

                Dispatcher.Instance.Invoke(() => OnHeartRateSensorConnected?.Invoke());
                OnHeartRateSensor?.Invoke();
            }
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        OnHeartRateSensor?.Invoke();
        WebsocketFactory factory = GetComponent<WebsocketFactory>();
        socket = factory.CreateWebSocket("ws://192.168.68.19:443/");
        socket.OnClose += Reconnect;
        socket.OnMessage += HandleData;
        socket.Connect();
    }

    private void HandleData(object sender, string data)
    {
        Debug.Log(data);
        var response = JsonUtility.FromJson<HeartData>(data);
        HeartRate = response.heartBeat;
        Debug.Log(HeartRate);
    }

    private void Reconnect(object o, EventArgs e)
    {
        Debug.Log("Reconnect");
        socket.Connect();
    }
}