using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStringEvent", menuName = "Events/String Event")]
public class StringEvent : ScriptableObject
{
    protected List<StringEventListener> listeners = new List<StringEventListener>();

    public void Raise(string _value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(this, _value);
    }

    public void RegisterListener(StringEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(StringEventListener listener)
    {
        listeners.Remove(listener);
    }
}