
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
        static readonly int IL = StoH("Idle_Left");
        static readonly int IR = StoH("Idle_Right");
        static readonly int WL = StoH("Walk_Left");
        static readonly int WR = StoH("Walk_Right");
        static readonly int UWL = StoH("Attack_Left");
        static readonly int UWR = StoH("Attack_Right");
        static readonly int UAL = StoH("Magic_Left");
        static readonly int UAR = StoH("Magic_Right");

        // for common anim
        public static Dictionary<Type, int[]> ANIM_NAMES = new Dictionary<Type, int[]>
        {
            { typeof(IdleState),       new int[2] { IL,  IR  } },
            { typeof(MoveState),       new int[2] { WL,  WR  } },
            { typeof(UseWeaponState),  new int[2] { UWL, UWR } },
            { typeof(UseAbilityState), new int[2] { UAL, UAR } },
        };

        public static AnimationClip[] PLAYER_ANIM_CLIPS;
        public static Sprite[] SPRITES_ICON1;
        public static Sprite[] SPRITES_ICON2;


        public static void LoadResources()
        {
            PLAYER_ANIM_CLIPS = Resources.LoadAll<AnimationClip>("Anims/Player");
            SPRITES_ICON1 = Resources.LoadAll<Sprite>("Arts/Items/Icons-1");
            SPRITES_ICON2 = Resources.LoadAll<Sprite>("Arts/Items/Icons-2");
        }

        private static int StoH(string s) => Animator.StringToHash(s);
    }
}