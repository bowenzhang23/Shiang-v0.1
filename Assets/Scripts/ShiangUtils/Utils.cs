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

        public static Item ItemLose(int n, ref Item toLose)
        {
            n = Math.Min(n, toLose.Count);

            var splitted = toLose.Clone();
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

        public static List<T1> GetSubclassesOf<T1>(params object[] ctorArgs)
        {
            List<T1> objects = new List<T1>();
            foreach (Type type in Assembly.GetAssembly(typeof(T1)).GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(T1))))
            {
                objects.Add((T1)Activator.CreateInstance(type, ctorArgs));
            }
            return objects;
        }

        public static SQLiteDatabase CreateSQLiteDatabase<T1>() where T1: SQLiteDatabase
        {
            var db = (T1)Activator.CreateInstance(typeof(T1));
            db.Create();
            return db;
        }

        public static SQLiteDatabase CreateSQLiteDatabase<T1>(string name) where T1 : SQLiteDatabase
        {
            var db = (T1)Activator.CreateInstance(typeof(T1), name);
            db.Create();
            return db;
        }

        public static void LoadEntityDatabase(string name,
            ref ItemContainer itemContainer)
        {
            Info.ENTITY_DB_COLLECTION[name] = CreateSQLiteDatabase<EntityDB>(name);
            if (!Info.ENTITY_DB_COLLECTION.TryGetValue(name, out var db))
                return;
            db.Retrieve();
            var data = (EntityData)db.Data;

            foreach (var itemCount in data.Items)
            {
                Item item = Pool.Items[itemCount.Key].Clone(itemCount.Value);
                itemContainer.ReceiveAll(ref item);
            }
        }

        public static void LoadEntityDatabase(string name, 
            ref ItemContainer itemContainer, 
            ref AbilityContainer abilityContainer)
        {
            Info.ENTITY_DB_COLLECTION[name] = CreateSQLiteDatabase<EntityDB>(name);
            if (!Info.ENTITY_DB_COLLECTION.TryGetValue(name, out var db))
                return;
            db.Retrieve();
            var data = (EntityData)db.Data;

            foreach (var itemCount in data.Items)
            {
                Item item = Pool.Items[itemCount.Key].Clone(itemCount.Value);
                itemContainer.ReceiveAll(ref item);
            }
            foreach (var abilityHash in data.Abilities)
            {
                var ability = Pool.Abilities[abilityHash];
                abilityContainer.Receive(ability);
            }
        }

        public static void SaveEntityDatabase(string name,
            ItemContainer inventory, 
            AbilityContainer abilityContainer)
        {
            var db =Info.ENTITY_DB_COLLECTION[name];

            db.Clear(); // clear first
            db.Insert(new EntityData()
            {
                Items = inventory.Data.ToDictionary(k => k.Hash, k => k.Count),
                Abilities = abilityContainer.Data.Select(k => k.Hash).ToList(),
            });
        }

        public static AnimationClip[] BuildClips(
            AnimationClip[] animationClips, string pattern) 
            => animationClips.Where(k => k.name.Contains(pattern))
                .OrderBy(k => k.name.Contains("Right")).ToArray();

        [Obsolete]
        public static Item ItemClonedFromPoolOfType<T1>()
            where T1 : Item, new()
            => Pool.Items[Pool.Mapping[typeof(T1)]].Clone();

        [Obsolete]
        public static T1 AbilityRefFromPoolOfType<T1>()
            where T1 : Ability, new()
            => (T1)Pool.Abilities[Pool.Mapping[typeof(T1)]];
    }
}