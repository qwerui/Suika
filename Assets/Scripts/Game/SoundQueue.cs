using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundQueue : MonoBehaviour
{
    private static SoundQueue instance = null;
    public static SoundQueue GetSoundQueue() => instance;

    AudioSource sfxSource;

    Queue<AudioClip> queue;
    HashSet<string> queuedClipNames;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            queue = new Queue<AudioClip>();
            queuedClipNames = new HashSet<string>();
            TryGetComponent(out sfxSource);
            DontDestroyOnLoad(gameObject);
        }    
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if(!queuedClipNames.Contains(clip.name))
        {
            queue.Enqueue(clip);
            queuedClipNames.Add(clip.name);
        }
    }

    private void Update()
    {
        if (queue.Count > 0)
        {
            while (queue.Count > 0)
            {
                sfxSource.PlayOneShot(queue.Dequeue());
            }
            queuedClipNames.Clear();
        }
    }
}
