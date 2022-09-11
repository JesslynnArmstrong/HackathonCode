using System;
using System.Linq;
using UnityEngine;

public class SessionService : MonoBehaviour
{
    private DateTime start;
    private Api api = new Api("https://future-of-healthcare-hackathon.herokuapp.com/");
    [SerializeField] private HeartRecorder recoder;

    // Start is called before the first frame update
    void Start()
    {
        start = DateTime.Now;
        recoder.Record();
    }

    // Update is called once per frame
    public void Publish()
    {
        recoder.Stop();
        api.Post("", JsonUtility.ToJson(new Session("0", $"Session {DateTime.Now}", start, DateTime.Now, recoder.Events.ToArray())),null);
    }

    private class Session
    {
        private string userId;
        private string sessionName;
        private DateTime startDate;
        private DateTime endDate;

        private Api.Event[] data;


        public Session(string userId, string sessionName, DateTime startDate, DateTime endDate, Api.Event[] data)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.sessionName = sessionName;
            this.userId = userId;
            this.data = data;
        }
    }
}
