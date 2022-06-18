
namespace Shiang
{
    public enum Orientation
    {
        Left = 0,
        Right = 1,
    }

    public interface IDynamic : IGameEntity
    {
        Orientation Orientation { get; }
        public void Move();
    }
}
