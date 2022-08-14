using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shiang
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class OnClickHoldButton : EventTrigger
    {
        private Coroutine routine = null;

        public override void OnPointerDown(PointerEventData data)
        {
            routine = StartCoroutine(Routine());
        }

        public IEnumerator Routine()
        {
            for (; ; )
            {
                GetComponent<Button>().onClick.Invoke();
                yield return null;
            }
        }

        public override void OnPointerUp(PointerEventData data)
        {
            StopCoroutine(routine);
        }
    }

    public class Persister
    {
        public Persister(IPersist persist) => Utils.RegisterForPersistenceAndLoad(persist);
    }

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

        public static SQLiteDatabase CreateSQLiteDatabase<T1>() where T1 : SQLiteDatabase
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

        public static IDatabase CreateDatabase<T1>() where T1 : IDatabase, new()
        {
            return SoundtrackDB.Instance;
        }

        public static void LoadEntityDatabase(string name,
            ref ItemContainer itemContainer)
        {
            AbilityContainer ac = null;
            LoadEntityDatabase(name, ref itemContainer, ref ac);
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

            if (abilityContainer == null) return;

            foreach (var abilityHash in data.Abilities)
            {
                var ability = Pool.Abilities[abilityHash];
                abilityContainer.Receive(ability);
            }
        }

        public static void SaveEntityDatabase(string name,
            ItemContainer inventory)
        {
            SaveEntityDatabase(name, inventory, null);
        }

        public static void SaveEntityDatabase(string name,
            ItemContainer inventory,
            AbilityContainer abilityContainer)
        {
            var db = Info.ENTITY_DB_COLLECTION[name];

            db.Clear(); // clear first
            db.Insert(new EntityData()
            {
                Items = inventory.Data.ToDictionary(k => k.Hash, k => k.Count),
                Abilities = abilityContainer == null ?
                    new List<uint>() : abilityContainer.Data.Select(k => k.Hash).ToList(),
            });
        }

        public static void RegisterForPersistenceAndLoad(IPersist persist)
        {
            persist.Load();
            Info.PERSIST_ENTITIES.Add(persist);
        }

        public static void SaveForPersistence()
            => Info.PERSIST_ENTITIES.ForEach(p => p.Save());

        public static AnimationClip[] BuildClips(
            AnimationClip[] animationClips, string pattern)
            => animationClips.Where(k => k.name.Contains(pattern))
                .OrderBy(k => k.name.Contains("Right")).ToArray();

        public static Item ItemClonedFromPoolOfType<T1>()
            where T1 : Item, new()
            => Pool.Items[Pool.Mapping[typeof(T1)]].Clone();

        public static T1 AbilityRefFromPoolOfType<T1>()
            where T1 : Ability, new()
            => (T1)Pool.Abilities[Pool.Mapping[typeof(T1)]];

        public static SoundtrackData GetSoundtrackByName(string name)
        {
            if (Info.SOUNDTRACK_DATA.TryGetValue(name, out var soundtrack))
                return soundtrack;
            return null;
        }

        public static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        public static void EventSystemFirstSelect(GameObject gameObject)
            => EventSystem.current.firstSelectedGameObject = gameObject;

        public static void EventSystemSelect(GameObject gameObject)
            => EventSystem.current.SetSelectedGameObject(gameObject);

        /// <summary>
        /// I stuck for a while and found the trick here:
        /// https://answers.unity.com/questions/1159573/eventsystemsetselectedgameobject-doesnt-highlight.html
        /// </summary>
        /// <param name="btn"></param>
        public static void HighlightButton(Button btn)
        {
            btn.Select();
            btn.OnSelect(null);
        }

        public static void SelectAndHighlightButton(Button btn)
        {
            EventSystemSelect(btn.gameObject);
            HighlightButton(btn);
        }

        public static Func<TResult> Bind<T, TResult>(Func<T, TResult> func, T arg)
        {
            return () => func(arg);
        }

        public static string GetAndroidExternalFilesDir()
        {
            using (AndroidJavaClass unityPlayer =
                   new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject context =
                       unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    // Get all available external file directories (emulated and sdCards)
                    AndroidJavaObject[] externalFilesDirectories =
                                        context.Call<AndroidJavaObject[]>
                                        ("getExternalFilesDirs", (object)null);
                    AndroidJavaObject emulated = null;
                    AndroidJavaObject sdCard = null;
                    for (int i = 0; i < externalFilesDirectories.Length; i++)
                    {
                        AndroidJavaObject directory = externalFilesDirectories[i];
                        using (AndroidJavaClass environment =
                               new AndroidJavaClass("android.os.Environment"))
                        {
                            // Check which one is the emulated and which the sdCard.
                            bool isRemovable = environment.CallStatic<bool>
                                              ("isExternalStorageRemovable", directory);
                            bool isEmulated = environment.CallStatic<bool>
                                              ("isExternalStorageEmulated", directory);
                            if (isEmulated)
                                emulated = directory;
                            else if (isRemovable && isEmulated == false)
                                sdCard = directory;
                        }
                    }
                    // Return the sdCard if available
                    if (sdCard != null)
                        return sdCard.Call<string>("getAbsolutePath");
                    else
                        return emulated.Call<string>("getAbsolutePath");
                }
            }
        }
    }
}

namespace Shiang
{
    public class TestDatabase
    {
        public void CreateDatabase()
        {
            var resourcePathDB = Utils.CreateSQLiteDatabase<ResourcePathDB>();
            var consumableDB = Utils.CreateSQLiteDatabase<ConsumableDB>();
            var weaponDB = Utils.CreateSQLiteDatabase<WeaponDB>();
            var abilityDB = Utils.CreateSQLiteDatabase<AbilityDB>();
            var entityDB = Utils.CreateSQLiteDatabase<EntityDB>("TestingEntity");

        }

        public void WriteToResourcePathDB()
        {
            var db = Utils.CreateSQLiteDatabase<ResourcePathDB>();
            db.Clear(); // clear first
            db.Insert(new ResourcePathData() { Name = "AbilityAnimClips", Path = "Anims/Ability" });
            db.Insert(new ResourcePathData() { Name = "WeaponAnimClips", Path = "Anims/Weapon" });
            db.Insert(new ResourcePathData() { Name = "SpritesIcon-1", Path = "Arts/Icons/Icons-1" });
            db.Insert(new ResourcePathData() { Name = "SpritesIcon-2", Path = "Arts/Icons/Icons-2" });
            db.Retrieve();
        }

        public void WriteToWeaponDB()
        {
            var db = Utils.CreateSQLiteDatabase<WeaponDB>();
            db.Clear(); // clear first
            db.Insert(new WeaponData()
            {
                ClassID = "Whip",
                Name = "长鞭",
                Description = "从小练习挥鞭",
                AnimPattern = "Whip",
                SoundtrackName = "attack_whip",
                CdTime = 1.1f,
                Hash = 0xE2000,
                SpriteIndex = 0
            });
            db.Insert(new WeaponData()
            {
                ClassID = "Fist",
                Name = " 拳头",
                Description = "朝我拳头跑来，我懒得过去打你",
                AnimPattern = "Attack",
                SoundtrackName = "attack_fist",
                CdTime = 1.1f,
                Hash = 0xE1011,
                SpriteIndex = 11
            });
            db.Insert(new WeaponData()
            {
                ClassID = "Axe",
                Name = "开山斧",
                Description = "祖传开山神斧",
                AnimPattern = "Axe",
                SoundtrackName = "attack_axe",
                CdTime = 2f,
                Hash = 0xE1014,
                SpriteIndex = 14
            });
            db.Insert(new WeaponData()
            {
                ClassID = "Broadsword",
                Name = "跋扈的大刀",
                Description = "一刀一个不管埋！",
                AnimPattern = "Broadsword",
                SoundtrackName = "attack_broadsword",
                CdTime = 4f,
                Hash = 0xEE000,
                SpriteIndex = 11
            });
            db.Retrieve();
        }

        public void WriteToAbilityDB()
        {
            var db = Utils.CreateSQLiteDatabase<AbilityDB>();
            db.Clear(); // clear first
            db.Insert(new AbilityData()
            {
                ClassID = "GoldenScepter",
                Name = "黄金权杖",
                Description = "堆中法器",
                AnimPattern = "GoldenScepter",
                SoundtrackName = "magic_goldenScepter",
                CdTime = 5f,
                Hash = 0xA1012,
                SpriteIndex = 12
            });
            db.Retrieve();
        }

        public void WriteToConsumableDB()
        {
            var db = Utils.CreateSQLiteDatabase<ConsumableDB>();
            db.Clear(); // clear first
            db.Insert(new ConsumableData()
            {
                ClassID = "OpuntiaFruit",
                Name = "仙人掌果",
                Description = "防风固土",
                Hash = 0x10024,
                SpriteIndex = 24
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "WangWangMilk",
                Name = "旺旺牛奶",
                Description = "茁壮成长",
                Hash = 0x10025,
                SpriteIndex = 25
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "SixDemon",
                Name = "六魔",
                Description = "沁人心脾",
                Hash = 0x10026,
                SpriteIndex = 26
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "LemonSoda",
                Name = "青柠苏打",
                Description = "安神解毒",
                Hash = 0x10027,
                SpriteIndex = 27
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "SnowCream",
                Name = "雪人雪糕",
                Description = "开始融化，面目狰狞",
                Hash = 0x10028,
                SpriteIndex = 28
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "LongJing",
                Name = "西湖龙井",
                Description = "清新健康，务必用此杯引用",
                Hash = 0x10029,
                SpriteIndex = 29
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "Ticket",
                Name = "门票",
                Description = "成人20，儿童500",
                Hash = 0x20001,
                SpriteIndex = 1
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "Bonbon",
                Name = "便携糖果",
                Description = "每次两粒，每日十次",
                Hash = 0x20003,
                SpriteIndex = 3
            });
            db.Insert(new ConsumableData()
            {
                ClassID = "Mints",
                Name = "薄荷糖",
                Description = "空罐，留有余香",
                Hash = 0x20004,
                SpriteIndex = 4
            });
            db.Retrieve();
        }

        public void WriteToEntityDB()
        {
            var db = Utils.CreateSQLiteDatabase<EntityDB>("RanRan");
            var itemsToInsert = new Dictionary<uint, int>();
            var abilitiesToInsert = new List<uint>();

            itemsToInsert.Add(0x10026, 10); // SixDemon
            itemsToInsert.Add(0xE2000, 1); // Whip

            abilitiesToInsert.Add(0xA1012);

            db.Clear(); // clear first
            db.Insert(new EntityData()
            {
                Items = itemsToInsert,
                Abilities = abilitiesToInsert
            });

            db.Retrieve();
        }

        public void WriteToEntityDBForFridge()
        {
            var db = Utils.CreateSQLiteDatabase<EntityDB>("Fridge-Test");
            var itemsToInsert = new Dictionary<uint, int>();
            var abilitiesToInsert = new List<uint>();

            itemsToInsert.Add(0x10024, 5); // 
            itemsToInsert.Add(0x10025, 5); // 
            itemsToInsert.Add(0x10026, 5); // 
            itemsToInsert.Add(0x10027, 5); // 
            itemsToInsert.Add(0x10028, 5); // 
            itemsToInsert.Add(0x10029, 5); // 
            itemsToInsert.Add(0x20001, 5); // 
            itemsToInsert.Add(0x20003, 5); // 
            itemsToInsert.Add(0x20004, 5); // 
            itemsToInsert.Add(0xE2000, 1); // Whip

            db.Clear(); // clear first
            db.Insert(new EntityData()
            {
                Items = itemsToInsert,
                Abilities = abilitiesToInsert
            });

            db.Retrieve();
        }
    }
}