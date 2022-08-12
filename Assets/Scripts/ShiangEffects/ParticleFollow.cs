
using UnityEngine;

namespace Shiang
{
    /// <summary>
    ///  Use two effects to cover the whole screen
    ///  They move according to the target's movement
    ///  So that it looks like the effects are everywhere
    /// </summary>
    public class ParticleFollow : MonoBehaviour
    {
        [Header("Margin")]
        [SerializeField] private float _margin = 5f;

        [Header("Two Effects")]
        [SerializeField] ParticleSystem _effect;
        ParticleSystem _effectA;
        ParticleSystem _effectB;

        private IPlayer _target;
        private float targetMoveX;
        private float effectY;
        private float effectZ;
        private float effectWidth;


        private void Awake()
        {
            _target = FindObjectOfType<RanRan>();
            effectWidth = _effect.shape.scale.x * _effect.transform.localScale.x;

            targetMoveX = _target.Orientation == Orientation.Right ? 1f : -1f;
            effectY = _target.Coordinate.y + _margin;
            effectZ = 0f; /// same level as target but render in foreground layer

            Vector3 positionA = new Vector3(_target.Coordinate.x, effectY, effectZ);
            _effectA = Instantiate(_effect, positionA, Quaternion.identity, transform);

            float xEffectB = positionA.x + targetMoveX * effectWidth;
            Vector3 positionB = new Vector3(xEffectB, effectY, effectZ);
            _effectB = Instantiate(_effect, positionB, Quaternion.identity, transform);
        }

        private void LateUpdate()
        {
            targetMoveX = _target.Orientation == Orientation.Right ? 1f : -1f;

            if (Mathf.Abs(_effectA.transform.position.x - _target.Coordinate.x) > effectWidth)
            {
                _effectA.transform.position += 2 * targetMoveX * effectWidth * Vector3.right;
            }

            if (Mathf.Abs(_effectB.transform.position.x - _target.Coordinate.x) > effectWidth)
            {
                _effectB.transform.position += 2 * targetMoveX * effectWidth * Vector3.right;
            }

        }
    }
}