
using Ink.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shiang
{
    #region state manager
    class RabbitStateManager : StateManager
    {
        IdleState _idle;
        FollowState _follow;
        Rabbit _rabbit;

        public override void InitStates()
        {
            if (_rabbit == null)
                _rabbit = (Rabbit)Owner;

            _idle = new IdleState(_rabbit);
            _follow = new FollowState(_rabbit);
        }

        public override void InitTransitions()
        {
            SM.AddTransiton(_idle, _follow, () => _rabbit.MeetFollowCriteria());
            SM.AddTransiton(_follow, _idle, () => !_rabbit.MeetFollowCriteria());
        }

        public override void SetInitialState() => SM.ChangeState(_idle);
    }
    #endregion

    [RequireComponent(typeof(DialogueTrigger))]
    public class Rabbit : MonoBehaviour, IFriend, ICreature, IDynamic, IFollower, IStoryTeller
    {
        // TODO
        private float _speed = 1.0f;

        StateManager _stateMgr;
        Animator _anim;
        Orientation _orientation;
        IPlayer _player;

        [SerializeField] DialogueData[] _dialogues;
        Dictionary<string, Story> _stories;
        DialogueTrigger _dialogueTrigger;

        public StateManager StateMgr => _stateMgr;

        public Animator Anim => _anim;
        
        public Orientation Orientation => _orientation;

        public AbilityContainer Abilities => null;

        public float StartFollowDistance => 10f;

        public float StopFollowDistance => 4f;

        public float PositionDiffToTarget => transform.position.x - _player.Coordinate.x;

        public Dictionary<string, Story> Stories => _stories;

        public void Idle()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(IdleState)][(int)_orientation]);
        }

        public void Move()
        {
            Anim.Play(Info.ANIM_NAMES[typeof(MoveState)][(int)_orientation]);
            _orientation = PositionDiffToTarget < 0 ? Orientation.Right : Orientation.Left;
            transform.position +=
                Vector3.right * (_orientation == Orientation.Right ? 1f : -1f) * _speed * Time.deltaTime;
        }

        public void UseAbility() { }

        public void Follow() => Move();

        private void Awake()
        {
            _orientation = Orientation.Left;
            _anim = GetComponent<Animator>();
            _stateMgr = Utils.CreateStateManager<RabbitStateManager, Rabbit>(this);
            _player = FindObjectOfType<RanRan>();
            _stories = _dialogues.ToDictionary(k => k.name, k => new Story(k.textAsset.text));
            _dialogueTrigger = GetComponent<DialogueTrigger>();
            _dialogueTrigger.CurrentStory = _stories["meeting"]; // TODO
        }

        void Update() => _stateMgr.Tick();

        public bool MeetFollowCriteria()
        {
            float distance = Mathf.Abs(PositionDiffToTarget);
            return distance < StartFollowDistance && distance > StopFollowDistance;
        }
    }
}