using UnityEngine;

namespace Shiang
{
    public interface IGameObject
    {
        string Description { get; }

        int Hash { get; }

        Sprite Image { get; }

        string Name { get; }
    }
}