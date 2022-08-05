
namespace Shiang
{
    interface IFollower
    {
        public float StartFollowDistance{ get; }

        public float StopFollowDistance{ get; }

        public float PositionDiff { get; }

        public void FollowPlayer();

        public bool MeetFollowCriteria();
    }
}
