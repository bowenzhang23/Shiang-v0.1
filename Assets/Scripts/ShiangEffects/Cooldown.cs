
using System.Collections;
using UnityEngine;

namespace Shiang
{
    public class Cooldown
    {
        float _time;
        float _count;
        bool _free;

        public Cooldown(float time)
        {
            Reset(time);
            _free = true;
        }

        public bool IsCooldown { get => _free; }

        public float Count { get => _count; }

        public void Reset() => _count = _time;

        public void Reset(float time) => _count = _time = time;

        public IEnumerator CountdownCo()
        {
            _free = false;
            while (_count > 0f)
            {
                _count -= Time.deltaTime;
                if (_count < 0) 
                    _count = 0f;
                yield return null;
            }
            Reset();
            _free = true;
        }
    }
}
