
using UnityEngine;


namespace Shiang
{
    /// <summary>
    ///  The background music player.
    ///  The script is implemented such that the musics
    ///  are played in a list and the list is played
    ///  on a loop.
    /// </summary>
    public class BackgroundMusic : GenericSingleton<BackgroundMusic>
    {
        [SerializeField] private string[] _clipNames; 
        private int _indexOfCurrentClip;
        private AudioSource _audioSource;

        public override void Awake()
        {
            base.Awake();
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.loop = false;
        }

        private void Update()
        {
            if (_audioSource.isPlaying)
                return;

            GetNextClip()?.AudioSourceSet(_audioSource);
            _audioSource.PlayDelayed(1f);
        }

        private SoundtrackData GetNextClip()
        {
            string clipName = _clipNames[_indexOfCurrentClip];
            _indexOfCurrentClip = (_indexOfCurrentClip + 1) % _clipNames.Length;
            return Utils.GetSoundtrackByName(clipName);
        }
    }
}