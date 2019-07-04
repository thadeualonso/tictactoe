using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class UnityEventString : UnityEvent<string> { }

public class StringEventListener : MonoBehaviour
{
    [Serializable]
    public class Listener
    {
        public string name;
        public StringEvent Event;
        public UnityEventString Response;
    }

    public List<Listener> listeners;

    private void OnEnable()
    {
        foreach (var listener in listeners)
            if (listener.Event != null)
                listener.Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        foreach (var listener in listeners)
            if (listener.Event != null)
                listener.Event.UnregisterListener(this);
    }

    public void OnEventRaised(StringEvent _event, string _value)
    {
        Listener listener = listeners.Find(x => x.Event == _event);
        listener?.Response.Invoke(_value);
    }
}