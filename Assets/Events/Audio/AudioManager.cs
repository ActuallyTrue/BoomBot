using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Code based on the audio manager from the milestones
/**
 * Usage guide:
 * AudioEvents is an array of audio events. It takes in a slug string that identifies the event and
 * the associated audio clip to play along with it
 * 
 * To use it, in the inspector of the Audio Manager, add an Audio Event (string + file). Then, in wherever
 * you want to play some audio, call the function
 * 
 * EventManager.TriggerEvent(YOUR_SLUG, position);
 * 
 * Currently, this manager only supports audio emitting from a particular location, but 
 * in the future it will likely be extended to support custom lambda functions, have more
 * configuration, playing audio with no position, etc.
 */
public class AudioManager : MonoBehaviour
{
    public EventSound3D eventSound3DPrefab;
    [System.Serializable]
    public struct AudioEvent
    {
        public string slug;
        public AudioClip audioClip;
        public float volume;
    }
    [SerializeField]
    public AudioEvent[] audioEvents;
    private Dictionary<string, UnityAction<Vector3>> listenersDictionary;
    void Awake()
    {
        listenersDictionary = new Dictionary<string, UnityAction<Vector3>>();
        foreach (AudioEvent audioEvent in audioEvents)
        {
            listenersDictionary.Add(audioEvent.slug, (Vector3 position) =>
            {
                if (eventSound3DPrefab)
                {
                    EventSound3D snd = Instantiate(eventSound3DPrefab, position, Quaternion.identity, null);
                    snd.audioSrc.clip = audioEvent.audioClip;
                    snd.audioSrc.volume = audioEvent.volume;
                    snd.audioSrc.minDistance = 5f;
                    snd.audioSrc.maxDistance = 100f;
                    snd.audioSrc.Play();
                }
            });
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (eventSound3DPrefab == null)
        {
            Debug.LogError("No Event Sound 3D Prefab");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        foreach (KeyValuePair<string, UnityAction<Vector3>> eventListener in listenersDictionary)
        {
            EventManager.StartListening(eventListener.Key, eventListener.Value);
        }
    }

    public void OnDisable()
    {
        foreach (KeyValuePair<string, UnityAction<Vector3>> eventListener in listenersDictionary)
        {
            EventManager.StopListening(eventListener.Key, eventListener.Value);
        }
    }
}
