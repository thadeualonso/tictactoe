using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Listener
{
    public string name;
    public GameEvent Event;
    public UnityEvent Response;
}

public class GameEventListener : MonoBehaviour, IGameEventListener
{
    public List<Listener> listeners = new List<Listener>();

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        foreach (var listener in listeners)
            listener.Event.RegisterListener(this);
    }

    private void UnsubscribeToEvents()
    {
        foreach (var listener in listeners)
            listener.Event.UnregisterListener(this);
    }

    public void OnEventRaised(GameEvent _event)
    {
        Listener listener = listeners.Find(x => x.Event == _event);
        listener?.Response.Invoke();
    }
}