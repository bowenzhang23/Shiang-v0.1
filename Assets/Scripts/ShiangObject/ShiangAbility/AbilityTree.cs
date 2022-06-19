using System;
using System.Collections;
using System.Collections.Generic;

namespace Shiang
{
    public class AbilityTree
    {
        public class Node
        {
            public Ability ability;
            public List<Node> followingNodes;

            public Node(Ability a, List<Node> f)
            {
                ability = a;
                followingNodes = f;
            }
        }

        Node _root;
        int _size;

        public AbilityTree(Node node)
        {
            _root = node;
            _size = 1;
        }

        public int Size { get => _size; }

        public Node FindNode(Type targetAbilityType)
        {
            return FindNodeDFS(_root, targetAbilityType);
        }

        private Node FindNodeDFS(Node current, Type targetAbilityType)
        {
            if (current.ability.GetType() == targetAbilityType)
                return current;

            foreach (var node in current.followingNodes)
            {
                Node n = FindNodeDFS(node, targetAbilityType);
                if (n != null) return n;
            }

            return null;
        }

        /// <summary>
        /// Add new ability with type <c>T1</c> next to the existed
        /// ability with type <c>existedAbilityType</c>
        /// If ability with type <c>T1</c> already exists, than do nothing
        /// </summary>
        /// <param name="existedAbilityType">Type of existed ability</param>
        /// <typeparam name="T1">Type of new ability</typeparam>
        public void AddAbilityNextTo<T1>(Type existedAbilityType) 
            where T1: Ability, new()
        {
            Node alreadyExist = FindNode(typeof(T1));
            if (alreadyExist != null) return;

            Node node = FindNode(existedAbilityType);
            if (node == null) return;

            Node newNode = new Node(new T1(), new List<Node>());
            node.followingNodes.Add(newNode);
            _size++;
        }
    }
}