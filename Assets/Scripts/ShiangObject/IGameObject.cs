using UnityEngine;

namespace Shiang
{
    public interface IGameObject
    {
        string Description { get; }

        uint Hash { get; }

        Sprite Image { get; }

        string Name { get; }
    }
}