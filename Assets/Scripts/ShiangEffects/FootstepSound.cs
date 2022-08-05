
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
    [RequireComponent(typeof(AudioSource))]
    public class FootstepSound : MonoBehaviour
    {
        Dictionary<GroundType, AudioClip[]> _footstepClips = 
            new Dictionary<GroundType, AudioClip[]>();

        // Add new ones here
        [SerializeField] AudioClip[] _normal;
        [SerializeField] AudioClip[] _snow;

        [SerializeField, Range(0, 1)] float _defaultVolume = 0.4f;
        [SerializeField, Range(0, 2)] float _defaultPitch = 1.2f;

        /// <summary>
        /// current clip cache
        /// </summary>
        AudioClip[] current;

        [SerializeField] GroundCheck _groundCheck;
        [SerializeField] AudioSource _audioSource;

        private void Awake()
        {
            // practical settings
            _audioSource.volume = _defaultVolume;
            _audioSource.pitch = _defaultPitch;

            // Add new ones here
            _footstepClips.Add(GroundType.Normal, _normal);
            _footstepClips.Add(GroundType.Snow, _snow);
        }

        /// <summary>
        /// The step function is called by the animator event.
        /// Thus this class must be attach to the same gameObject 
        /// as the animator.
        /// </summary>
        public void Step()
        {
            GroundType type = _groundCheck.CurrentGroundType;
            if (type == GroundType.None) return;
            // Debug.Log(type);
            current = _footstepClips[type];
            AudioClip clip = GetRandomClip();
            _audioSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Add a bit of randomness of footstep sound
        /// </summary>
        /// <returns>One of the clip, usually from a total of four.</returns>
        private AudioClip GetRandomClip()
        {
            return current[UnityEngine.Random.Range(0, current.Length)];
        }
    }
}