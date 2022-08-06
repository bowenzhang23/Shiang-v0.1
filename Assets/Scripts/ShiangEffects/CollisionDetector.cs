
using UnityEngine;

namespace Shiang
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionDetector : MonoBehaviour
    {
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
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<RanRan>(out _))
            {
                PlayerDetected = false;
            }
        }
    }
}
