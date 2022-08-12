
using System.Collections.Generic;
using UnityEngine;


namespace Shiang {
    /// <summary>
    ///  Play sound effect according to the ground that the player is 
    ///  standing on. The type of ground is checked by the 
    ///  <c>GroundCheck</c> script.
    /// 
    /// </summary>
    /// 
    /// <seealso cref="Shiang.GroundCheck"/>
    /// 
    public class FootstepSound : MonoBehaviour
    {
        Dictionary<GroundType, string[]> _footstepClips = 
            new Dictionary<GroundType, string[]>();

        // Add new ones here
        [SerializeField] string[] _normal;
        [SerializeField] string[] _snow;
        string[] _current;

        AudioSource _audioSource;
        [SerializeField] GroundCheck _groundCheck;

        private void Awake()
        {
            // Add new ones here
            _footstepClips.Add(GroundType.Normal, _normal);
            _footstepClips.Add(GroundType.Snow, _snow);
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.loop = false;
        }

        /// <summary>
        /// The step function is called by the animator event.
        /// Thus this class must be attach to the same gameObject 
        /// as the animator.
        /// </summary>
        public void Step()
        {
            if (_audioSource.isPlaying)
                return;

            GroundType type = _groundCheck.CurrentGroundType;
            if (type == GroundType.None) 
                return;
            
            _current = _footstepClips[type];
            var soundtrack = GetRandomSoundtrack();
            soundtrack.AudioSourceSet(_audioSource);
            _audioSource.Play();
        }

        /// <summary>
        /// Add a bit of randomness of footstep sound
        /// </summary>
        /// <returns>One of the clip, usually from a total of four.</returns>
        private SoundtrackData GetRandomSoundtrack()
            => Utils.GetSoundtrackByName(_current[Random.Range(0, _current.Length)]);
    }
}