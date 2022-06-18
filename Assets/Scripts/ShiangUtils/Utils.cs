using System;
using System.Collections.Generic;

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

        public static AbilityTree CreatAbilityTree(Ability ability) 
            => new AbilityTree(new AbilityTree.Node(ability, new List<AbilityTree.Node>()));
    }
}