// https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events#

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{

    private Dictionary<string, UnityEventBase> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEventBase>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            UnityEvent e = thisEvent as UnityEvent;
            e.AddListener(listener);
        }
        else
        {
            UnityEvent e = new UnityEvent();
            e.AddListener(listener);
            instance.eventDictionary.Add(eventName, e);
        }
    }
    public static void StartListening<T0>(string eventName, UnityAction<T0> listener)
    {

        UnityEventBase thisEvent = null;

        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            (thisEvent as UnityEvent<T0>).AddListener(listener);
        }
        else
        {
            UnityEvent<T0> e = new UnityEvent<T0>();
            e.AddListener(listener);
            instance.eventDictionary.Add(eventName, e);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            UnityEvent e = thisEvent as UnityEvent;
            e.RemoveListener(listener);
        }
    }

    public static void StopListening<T0>(string eventName, UnityAction<T0> listener)
    {
        if (eventManager == null) return;
        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            UnityEvent<T0> e = thisEvent as UnityEvent<T0>;
            e.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            UnityEvent e = thisEvent as UnityEvent;
            e.Invoke();
        }
    }

    public static void TriggerEvent<T0>(string eventName, T0 t0_obj)
    {
        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            UnityEvent<T0> e = thisEvent as UnityEvent<T0>;
            e.Invoke(t0_obj);
        }
    }
}