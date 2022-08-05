using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shiang
{
    public class CoolState : IState
    {
        IPlayer _player;

        public CoolState(IPlayer player) => _player = player;
        
        public void Enter() => _player.Cool();

        public void Exit() { }

        public void Tick() { }
    }
}