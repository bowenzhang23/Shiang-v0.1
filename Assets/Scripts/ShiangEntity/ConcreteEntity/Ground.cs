using UnityEngine;

namespace Shiang
{
    public enum GroundType
    {
        None,
        Normal,
        Snow,
    }

    /// <summary>
    /// Handle the ground type and etc.
    /// </summary>
    public class Ground : MonoBehaviour, IStatic
    {
        [SerializeField] GroundType type = GroundType.None;
        public GroundType Type { get => type; private set => type = value; }

        public void Idle() { }

    }
}