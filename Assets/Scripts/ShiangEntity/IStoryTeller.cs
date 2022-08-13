
using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    [System.Serializable]
    public class DialogueData
    {
        public string name;
        public TextAsset textAsset;
    }

    public interface IStoryTeller
    {
        public Dictionary<string, Story> Stories { get; }
    }
}
