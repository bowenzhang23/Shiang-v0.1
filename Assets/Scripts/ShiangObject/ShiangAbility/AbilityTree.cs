using System.Collections;
using System.Collections.Generic;

namespace Shiang
{
    public class AbilityTree
    {
        public class Node
        {
            public Ability ability;
            public List<Node> following;

            public Node(Ability a, List<Node> f)
            {
                ability = a;
                following = f;
            }
        }

        Node _root;

        public AbilityTree(Node node)
        {
            _root = node;
        }
    }
}