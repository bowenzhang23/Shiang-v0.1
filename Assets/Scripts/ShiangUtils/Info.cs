
using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Shiang
{
    /// <summary>
    ///  A static class (cannot be instantiated) that holds
    ///  the conventional names for that state.
    /// </summary>
    public static class Info
    {
        public static SQLiteDatabase RESOURCEPATH_DB;
        public static SQLiteDatabase CONSUMABLE_DB;
        public static SQLiteDatabase WEAPON_DB;
        public static SQLiteDatabase ABILITY_DB;
        public static Dictionary<string, SQLiteDatabase> ENTITY_DB_COLLECTION = 
            new Dictionary<string, SQLiteDatabase>();

        static readonly int OPEN = StoH("Open");
        static readonly int CLOSE = StoH("Close");
        static readonly int IDLE_LEFT = StoH("Idle_Left");
        static readonly int IDLE_RIGHT = StoH("Idle_Right");
        static readonly int WALK_LEFT = StoH("Walk_Left");
        static readonly int WALK_RIGHT = StoH("Walk_Right");
        static readonly int COOL_LEFT = StoH("Cool_Left");
        static readonly int COOL_RIGHT = StoH("Cool_Right");

        // for common anim
        public static Dictionary<Type, int[]> ANIM_NAMES = new Dictionary<Type, int[]>
        {
            { typeof(OpenState),       new int[2] { CLOSE,      OPEN        } },      
            { typeof(IdleState),       new int[2] { IDLE_LEFT,  IDLE_RIGHT  } },
            { typeof(MoveState),       new int[2] { WALK_LEFT,  WALK_RIGHT  } },
            { typeof(CoolState),       new int[2] { COOL_LEFT,  COOL_RIGHT  } },
        };

        public static Dictionary<Type, AnimationClip[]> ANIM_CLIPS = new Dictionary<Type, AnimationClip[]>();
        public static Sprite[] SPRITES_ICON1;
        public static Sprite[] SPRITES_ICON2;

        public static Dictionary<string, string> RESOURCE_DATA;
        public static Dictionary<string, ConsumableData> CONSUMABLE_DATA;
        public static Dictionary<string, WeaponData> WEAPON_DATA;
        public static Dictionary<string, AbilityData> ABILITY_DATA;

        /// <summary>
        /// Load this before <c>LoadResources</c>
        /// </summary>
        public static void LoadDatabase()
        {
            RESOURCEPATH_DB = Utils.CreateSQLiteDatabase<ResourcePathDB>();
            CONSUMABLE_DB = Utils.CreateSQLiteDatabase<ConsumableDB>();
            WEAPON_DB = Utils.CreateSQLiteDatabase<WeaponDB>();
            ABILITY_DB = Utils.CreateSQLiteDatabase<AbilityDB>();

            RESOURCEPATH_DB.Retrieve();
            CONSUMABLE_DB.Retrieve();
            WEAPON_DB.Retrieve();
            ABILITY_DB.Retrieve();

            RESOURCE_DATA = ((List<ResourcePathData>)RESOURCEPATH_DB.Data).ToDictionary(k => k.Name, k => k.Path);
            CONSUMABLE_DATA = ((List<ConsumableData>)CONSUMABLE_DB.Data).ToDictionary(k => k.ClassID, k => k);
            WEAPON_DATA = ((List<WeaponData>)WEAPON_DB.Data).ToDictionary(k => k.ClassID, k => k);
            ABILITY_DATA = ((List<AbilityData>)ABILITY_DB.Data).ToDictionary(k => k.ClassID, k => k);
        }

        public static void LoadResources()
        {
            ANIM_CLIPS[typeof(RanRan)] = Resources.LoadAll<AnimationClip>(RESOURCE_DATA["PlayerAnimClips"]);
            ANIM_CLIPS[typeof(Rabbit)] = Resources.LoadAll<AnimationClip>(RESOURCE_DATA["RabbitAnimClips"]);
            ANIM_CLIPS[typeof(Ability)] = Resources.LoadAll<AnimationClip>(RESOURCE_DATA["AbilityAnimClips"]);
            ANIM_CLIPS[typeof(Weapon)] = Resources.LoadAll<AnimationClip>(RESOURCE_DATA["WeaponAnimClips"]);
            SPRITES_ICON1 = Resources.LoadAll<Sprite>(RESOURCE_DATA["SpritesIcon-1"]);
            SPRITES_ICON2 = Resources.LoadAll<Sprite>(RESOURCE_DATA["SpritesIcon-2"]);
        }

        private static int StoH(string s) => Animator.StringToHash(s);
    }
}