using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DialogData {
    public string id;
    public string lang;
    public string sessionId;
    public string timestamp;
    public Result result;
    public Status status;

    [System.Serializable]
    public class Parameters
    {
        public string location;
    }
    [System.Serializable]
    public class Location
    {
        public string location;
        public string original;
    }
    [System.Serializable]
    public class Context
    {
        public string name;
        public int lifespan;
        public Location parameters;
    }
    [System.Serializable]
    public class Metadata
    {
        public string intentId;
        public string intentName;
        public string webhookUsed;
        public string webhookForSlotFillingUsed;
        public string isFallbackIntent;
    }
    [System.Serializable]
    public class Message
    {
        public string lang;
        public int type;
        public string speech;
    }
    [System.Serializable]
    public class Fulfillment
    {
        public string speech;
        public List<Message> messages;
    }


    [System.Serializable]
    public class Result
    {
        public string source;
        public string resolvedQuery;
        public string action;
        public bool actionIncomplete;
        public double score;
        public Parameters parameters;
        public List<Context> contexts;
        public Metadata metadata;
        public Fulfillment fulfillment;
    }
    [System.Serializable]
    public class Status
    {
        public int code;
        public string errorType;
    }
        public static DialogData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<DialogData>(jsonString);
    }
}
