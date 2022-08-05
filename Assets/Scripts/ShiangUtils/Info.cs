
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
        public static ResourcePathDB RESOURCEPATH_DB;
        public static ConsumableDB CONSUMABLE_DB;
        public static WeaponDB WEAPON_DB;
        public static AbilityDB ABILITY_DB;

        static readonly int IL = StoH("Idle_Left");
        static readonly int IR = StoH("Idle_Right");
        static readonly int WL = StoH("Walk_Left");
        static readonly int WR = StoH("Walk_Right");
        static readonly int CL = StoH("Cool_Left");
        static readonly int CR = StoH("Cool_Right");
        static readonly int UWL = StoH("Attack_Left");
        static readonly int UWR = StoH("Attack_Right");
        static readonly int UAL = StoH("Magic_Left");
        static readonly int UAR = StoH("Magic_Right");

        // for common anim
        public static Dictionary<Type, int[]> ANIM_NAMES = new Dictionary<Type, int[]>
        {
            { typeof(IdleState),       new int[2] { IL,  IR  } },
            { typeof(MoveState),       new int[2] { WL,  WR  } },
            { typeof(CoolState),       new int[2] { CL,  CR  } },
            { typeof(UseWeaponState),  new int[2] { UWL, UWR } },
            { typeof(UseAbilityState), new int[2] { UAL, UAR } },
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
            RESOURCEPATH_DB = Utils.CreateDatabase<ResourcePathDB>();
            CONSUMABLE_DB = Utils.CreateDatabase<ConsumableDB>();
            WEAPON_DB = Utils.CreateDatabase<WeaponDB>();
            ABILITY_DB = Utils.CreateDatabase<AbilityDB>();

            RESOURCEPATH_DB.Retrieve();
            CONSUMABLE_DB.Retrieve();
            WEAPON_DB.Retrieve();
            ABILITY_DB.Retrieve();

            RESOURCE_DATA = RESOURCEPATH_DB.Data.ToDictionary(k => k.Name, k => k.Path);
            CONSUMABLE_DATA = CONSUMABLE_DB.Data.ToDictionary(k => k.ClassID, k => k);
            WEAPON_DATA = WEAPON_DB.Data.ToDictionary(k => k.ClassID, k => k);
            ABILITY_DATA = ABILITY_DB.Data.ToDictionary(k => k.ClassID, k => k);
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