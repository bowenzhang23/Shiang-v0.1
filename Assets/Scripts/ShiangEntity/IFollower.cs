
namespace Shiang
{
    interface IFollower
    {
        public float StartFollowDistance{ get; }

        public float StopFollowDistance{ get; }

        public float PositionDiffToTarget { get; }

        public void Follow();

        public bool MeetFollowCriteria();
    }
}
