
namespace Shiang
{
    interface IVivid
    {
        public string[] ClipNames { get; }
        public string SoundtrackName { get; }
        public float ClipLength { get; }
        public Cooldown Cd { get; }
    }
}
