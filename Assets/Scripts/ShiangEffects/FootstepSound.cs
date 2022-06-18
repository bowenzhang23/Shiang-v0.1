
using System.Collections.Generic;
using UnityEngine;


namespace Shiang {
    /// <summary>
    ///  Play sound effect according to the ground that the player is 
    ///  standing on. The type of ground is checked by the 
    ///  <c>GroundCheck</c> script.
    /// <note type="note">
    ///  This sound effect must work with a <c>AudioSource</c>.
    /// </note>
    /// 
    /// </summary>
    /// 
    /// <seealso cref="Shiang.GroundCheck"/>
    /// 
    [RequireComponent(typeof(AudioSource))]
    public class FootstepSound : MonoBehaviour
    {
        private Dictionary<GroundType, AudioClip[]> footstepClips = 
            new Dictionary<GroundType, AudioClip[]>();

        // Add new ones here
        [SerializeField] private AudioClip[] normal;
        [SerializeField] private AudioClip[] snow;

        [SerializeField, Range(0, 1)] private float defaultVolume = 0.4f;
        [SerializeField, Range(0, 2)] private float defaultPitch = 1.2f;

        /// <summary>
        /// current clip cache
        /// </summary>
        private AudioClip[] current;

        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private AudioSource audioSource;

        private void Awake()
        {
            // practical settings
            audioSource.volume = defaultVolume;
            audioSource.pitch = defaultPitch;

            // Add new ones here
            footstepClips.Add(GroundType.Normal, normal);
            footstepClips.Add(GroundType.Snow, snow);
        }

        /// <summary>
        /// The step function is called by the animator event.
        /// Thus this class must be attach to the same gameObject 
        /// as the animator.
        /// </summary>
        public void Step()
        {
            GroundType type = groundCheck.CurrentGroundType;
            if (type == GroundType.None) return;
            // Debug.Log(type);
            current = footstepClips[type];
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
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