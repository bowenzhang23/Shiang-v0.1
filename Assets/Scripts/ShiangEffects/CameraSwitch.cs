
using UnityEngine;

namespace Shiang
{
    /// <summary>
    /// The states of state driven camera need the animator based state machine
    /// So in this particular case our state machine is not used
    /// </summary>
    public class CameraSwitch : MonoBehaviour
    {
        Animator _anim;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (InputController.Instance.CameraSwitch)
            {
                _anim.SetTrigger("switch");
            }
        }
    }
}