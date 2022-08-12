using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    [System.Serializable]
    public class SoundtrackData
    {
        public string name;
        public AudioClip clip;
        [Range(0, 1)] public float volumn;
        [Range(0.1f, 3)] public float pitch;

        public void AudioSourceSet(AudioSource audioSource)
        {
            audioSource.clip = clip;
            audioSource.volume = volumn;
            audioSource.pitch = pitch;
        }
    }


    public class SoundtrackDB : GenericSingleton<SoundtrackDB>, IDatabase
    {
        [SerializeField] SoundtrackData[] _soundTracks;

        public object Data => _soundTracks;

        public void Clear()
        {
            _soundTracks = null;
        }

        public void Create() { }

        public void Insert(object entry) { }

        public void Retrieve() { }

    }
}
