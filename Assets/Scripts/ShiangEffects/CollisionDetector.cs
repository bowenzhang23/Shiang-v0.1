
using System;
using UnityEngine;

namespace Shiang
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionDetector : MonoBehaviour
    {
        public event Action OnPlayerEnter;
        public event Action OnPlayerStay;
        public event Action OnPlayerExit;

        private void Awake()
        {
            PlayerDetected = false;
        }

        public bool PlayerDetected { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<RanRan>(out _))
            {
                PlayerDetected = true;
                OnPlayerEnter?.Invoke();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent<RanRan>(out _))
            {
                OnPlayerStay?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<RanRan>(out _))
            {
                PlayerDetected = false;
                OnPlayerExit?.Invoke();
            }
        }
    }
}
