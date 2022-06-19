using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    public class AbilityContainer
    {
        List<Ability> _abilities = new List<Ability>();
        int _capacity = 3;

        public AbilityContainer() { }

        public AbilityContainer(int capacity) => _capacity = capacity;

        public event Action OnFull;

        public int Size() => _abilities.Count;

        public int Capacity() => _capacity;

        public void Receive(Ability ability)
        {
            if (Find(ability, out var _))
                return;
            else if (Size() == Capacity())
                OnFull?.Invoke();
            else
                _abilities.Add(ability);
        }

        public void Remove(Ability ability)
        {
            if (Find(ability, out var exist))
                _abilities.Remove(exist);
        }

        public void Remove(Type abilityType)
        {
            if (Find(abilityType, out var exist))
                _abilities.Remove(exist);
        }

        public bool IsEmpty() => Size() == 0;

        public bool Find(Ability ability, out Ability exist)
        {
            exist = _abilities.Find((Ability a) => a.Hash == ability.Hash);
            if (exist != null)
                return true;
            return false;
        }

        public bool Find(Type abilityType, out Ability exist)
        {
            exist = _abilities.Find((Ability a) => a.GetType() == abilityType);
            if (exist != null)
                return true;
            return false;
        }

        public List<Ability> Abilities() => _abilities;
    }
}