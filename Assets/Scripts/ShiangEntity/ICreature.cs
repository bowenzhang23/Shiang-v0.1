using UnityEngine;

namespace Shiang
{
    public interface ICreature : 
        IAttackable, ISkillFul, IHurtable, IStateHolder, IAnimatorHolder
    {

    }
}