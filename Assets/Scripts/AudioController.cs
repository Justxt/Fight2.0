using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]

public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;
    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.loop = loop;
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}


public class AudioController : MonoBehaviour
{

    public static AudioController audioController;

    [SerializeField]
    private Sound[] sounds;


    private void Awake()
    {

        if (audioController == null)
        {
            DontDestroyOnLoad(gameObject);
            audioController = this;
        }
        else
        {
            if (audioController != this)
            {
                Destroy(gameObject);
            }
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void PlayBGMSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Stop();
        }
    }
}
