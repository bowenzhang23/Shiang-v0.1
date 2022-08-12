
using UnityEngine;

namespace Shiang
{
    class SoundEffect : MonoBehaviour
    {
        AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.loop = false;
        }

        public void Play(string name)
        {
            var soundtrack = Utils.GetSoundtrackByName(name);
            soundtrack.AudioSourceSet(_audioSource);
            _audioSource.Play();
        }
    }
}
