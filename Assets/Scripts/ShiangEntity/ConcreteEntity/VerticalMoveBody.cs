
using UnityEngine;

namespace Shiang
{
    public class VerticalMoveBody : MonoBehaviour, IDynamic
    {
        [SerializeField] Transform _topPosition;
        [SerializeField] Transform _buttomPosition;
        [SerializeField, Range(0f, 0.5f)] float _speed;

        float ymax;
        float ymin;

        public Orientation Orientation => 0;

        public void Idle() { }

        public void Move()
        {
            if (transform.position.y > ymax || transform.position.y < ymin)
                _speed *= -1f;
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }

        private void Awake()
        {
            ymax = _topPosition.position.y;
            ymin = _buttomPosition.position.y;
        }

        void Update() => Move();
    }
}