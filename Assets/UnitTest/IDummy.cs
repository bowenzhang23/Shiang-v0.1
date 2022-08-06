using UnityEngine;
using Shiang;

namespace ShiangTest
{
    public interface IDummy { }

    #region Dummy Abilities
    public class A1 : Ability, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFFF;
        public override Sprite Image => null;
        public override string Name => "A1";
        public override float CdTime => 1f;
        public override void Affect(IGameEntity entity) { }
    }

    public class A2 : Ability, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFFE;
        public override Sprite Image => null;
        public override string Name => "A2";
        public override float CdTime => 1f;
        public override void Affect(IGameEntity entity) { }
    }

    public class A3 : Ability, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFFD;
        public override Sprite Image => null;
        public override string Name => "A4";
        public override float CdTime => 1f;
        public override void Affect(IGameEntity entity) { }
    }

    public class A4 : Ability, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFFC;
        public override Sprite Image => null;
        public override string Name => "A4";
        public override float CdTime => 1f;
        public override void Affect(IGameEntity entity) { }
    }

    public class A5 : Ability, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFFB;
        public override Sprite Image => null;
        public override string Name => "A5";
        public override float CdTime => 1f;
        public override void Affect(IGameEntity entity) { }
    }

    public class A6 : Ability, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFFA;
        public override Sprite Image => null;
        public override string Name => "A6";
        public override float CdTime => 1f;
        public override void Affect(IGameEntity entity) { }
    }

    public class A7 : Ability, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFF9;
        public override Sprite Image => null;
        public override string Name => "A7";
        public override float CdTime => 1f;
        public override void Affect(IGameEntity entity) { }
    }

    public class A8 : Ability, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFF8;
        public override Sprite Image => null;
        public override string Name => "A8";
        public override float CdTime => 1f;
        public override void Affect(IGameEntity entity) { }
    }
    #endregion


    #region Dummy Items
    public class I1 : Consumable, IDummy
    {
        public override string Description => "";
        public override uint Hash => 0xFFFFFFEF;
        public override Sprite Image => null;
        public override string Name => "I1";
        public override Item Clone(int n) => new I1 { Count = n };
    }

    public class I2 : Consumable, IDummy
    {
        public override string Description => "";
        public override uint Hash => 0xFFFFFFEE;
        public override Sprite Image => null;
        public override string Name => "I2";
        public override Item Clone(int n) => new I2 { Count = n };
    }

    public class I3 : Consumable, IDummy
    {
        public override string Description => "";
        public override uint Hash => 0xFFFFFFED;
        public override Sprite Image => null;
        public override string Name => "I3";
        public override Item Clone(int n) => new I3 { Count = n };
    }

    public class I4 : Consumable, IDummy
    {
        public override string Description => "";
        public override uint Hash => 0xFFFFFFEC;
        public override Sprite Image => null;
        public override string Name => "I4";
        public override Item Clone(int n) => new I4 { Count = n };
    }

    public class W1 : Weapon, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFEB;
        public override Sprite Image => null;
        public override string Name => "W1";
        public override float CdTime => 1f;
        public override void Hit(IHurtable hurtable) { }
        public override Item Clone(int n) => new W1 { Count = n };
    }

    public class W2 : Weapon, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFEA;
        public override Sprite Image => null;
        public override string Name => "W2";
        public override float CdTime => 1f;
        public override void Hit(IHurtable hurtable) { }
        public override Item Clone(int n) => new W2 { Count = n };
    }

    public class W3 : Weapon, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFE9;
        public override Sprite Image => null;
        public override string Name => "W3";
        public override float CdTime => 1f;
        public override void Hit(IHurtable hurtable) { }
        public override Item Clone(int n) => new W3 { Count = n };
    }

    public class W4 : Weapon, IDummy
    {
        public override AnimationClip[] Clips => null;
        public override string Description => "";
        public override uint Hash => 0xFFFFFFE8;
        public override Sprite Image => null;
        public override string Name => "W4";
        public override float CdTime => 1f;
        public override void Hit(IHurtable hurtable) { }
        public override Item Clone(int n) => new W4 { Count = n };
    }

    public class Ar1 : Armor, IDummy
    {
        public override string Description => "";
        public override uint Hash => 0xFFFFFFE7;
        public override Sprite Image => null;
        public override string Name => "Ar1";
        public override Item Clone(int n) => new Ar1 { Count = n };
    }

    public class Ar2 : Armor, IDummy
    {
        public override string Description => "";
        public override uint Hash => 0xFFFFFFE6;
        public override Sprite Image => null;
        public override string Name => "Ar2";
        public override Item Clone(int n) => new Ar2 { Count = n };
    }

    public class Ar3 : Armor, IDummy
    {
        public override string Description => "";
        public override uint Hash => 0xFFFFFFE5;
        public override Sprite Image => null;
        public override string Name => "Ar3";
        public override Item Clone(int n) => new Ar3 { Count = n };
    }

    public class Ar4 : Armor, IDummy
    {
        public override string Description => "";
        public override uint Hash => 0xFFFFFFE4;
        public override Sprite Image => null;
        public override string Name => "Ar4";
        public override Item Clone(int n) => new Ar4 { Count = n };
    }
    #endregion

}