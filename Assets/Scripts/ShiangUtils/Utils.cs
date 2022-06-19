using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Shiang
{
    public static class Utils
    {
        /// <summary>
        /// Factory method that creates State manager that involves <c>InputController</c>
        /// </summary>
        /// <typeparam name="T1">Type of StateManager</typeparam>
        /// <typeparam name="T2">Type of IGameEntity</typeparam>
        /// <param name="entity">The IGameEntity object</param>
        /// <returns>A StateManager object</returns>
        public static T1 CreateStateManager<T1, T2>(T2 entity) 
            where T1 : StateManager, new() where T2 : IGameEntity
        {
            T1 manager = Activator.CreateInstance<T1>(); 
            manager.SetOwner(entity);
            manager.InitStates();
            manager.InitTransitions();
            manager.SetInitialState();
            return manager;
        }

        /// <summary>
        /// Factory method that creates State manager that involves <c>InputController</c>
        /// </summary>
        /// <typeparam name="T1">Type of StateManagerIC</typeparam>
        /// <typeparam name="T2">Type of IGameEntity</typeparam>
        /// <param name="entity">The IGameEntity object</param>
        /// <param name="ic">The InputController object</param>
        /// <returns>A StateManagerIC object</returns>
        public static T1 CreateStateManagerIC<T1, T2>(T2 entity, InputController ic)
            where T1 : StateManagerIC, new() where T2 : IGameEntity
        {
            T1 manager = Activator.CreateInstance<T1>();
            manager.SetInputController(ic);
            manager.SetOwner(entity);
            manager.InitStates();
            manager.InitTransitions();
            manager.SetInitialState();
            return manager;
        }

        public static T1 ItemLose<T1>(int n, ref T1 toLose) where T1: Item, new()
        {
            n = Math.Min(n, toLose.Count);

            var splitted = toLose.Clone<T1>();
            splitted.Count = n;
            toLose.Count -= n;
            
            if (toLose.Count == 0) 
                toLose = null;

            return splitted;
        }

        public static void ItemMerge(Item item, ref Item beMerged)
        {
            if (item.GetType() != beMerged.GetType())
                return;

            item.Count += beMerged.Count;
            beMerged = null;
        }

        public static ItemContainer CreateItemContainer()
            => new ItemContainer();

        public static ItemContainer CreateItemContainer(int capacity)
            => new ItemContainer(capacity);

        public static AbilityContainer CreateAbilityContainer()
            => new AbilityContainer();

        public static AbilityContainer CreateAbilityContainer(int capacity)
            => new AbilityContainer(capacity);

        public static AbilityTree CreateAbilityTree(Ability ability) 
            => new AbilityTree(new AbilityTree.Node(ability, new List<AbilityTree.Node>()));

        public static List<T1> GetListOfType<T1>(params object[] ctorArgs)
        {
            List<T1> objects = new List<T1>();
            foreach (Type type in Assembly.GetAssembly(typeof(T1)).GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(T1))))
            {
                objects.Add((T1)Activator.CreateInstance(type, ctorArgs));
            }
            return objects;
        }

        public static T1 CreateDatabase<T1>() where T1 : IDatabase, new()
        {
            var db = Activator.CreateInstance<T1>();
            db.Create();
            return db;
        }

        public static AnimationClip[] BuildClips(
            AnimationClip[] animationClips, string pattern) 
            => animationClips.Where(k => k.name.Contains(pattern))
                .OrderBy(k => k.name.Contains("Right")).ToArray();
    }
}