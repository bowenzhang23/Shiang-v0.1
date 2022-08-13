
using System.Collections;
using UnityEngine;
using Cinemachine;

namespace Shiang
{
    public class CameraZoom : MonoBehaviour
    {
        private const float _OFFSET = 0.01f;
        CinemachineVirtualCamera _cam;
        float _defaultFoV; // 40f

        [SerializeField, Range(0, 50)] float _zoomInSpeed = 6f;
        [SerializeField, Range(0, 50)] float _zoomOutSpeed = 16f;
        [SerializeField, Range(0, 1)] float _zoomRate = 0.75f;

        void Awake()
        {
            _cam = GetComponent<CinemachineVirtualCamera>();
            _defaultFoV = _cam.m_Lens.FieldOfView;
        }

        void Update()
        {
            if (InputController.Instance.CameraZoomIn)
            {
                _cam.m_Lens.FieldOfView = Mathf.SmoothStep(
                    _cam.m_Lens.FieldOfView, _defaultFoV * _zoomRate, _zoomInSpeed * Time.deltaTime);
            }
            else if (InputController.Instance.CameraZoomOut)
            {
                StartCoroutine(CameraRecoverCo());
            }
        }

        private IEnumerator CameraRecoverCo()
        {
            while (_cam.m_Lens.FieldOfView < _defaultFoV - _OFFSET)
            {
                _cam.m_Lens.FieldOfView = Mathf.SmoothStep(
                    _cam.m_Lens.FieldOfView, _defaultFoV, _zoomOutSpeed * Time.deltaTime);
                yield return null;
            }
            _cam.m_Lens.FieldOfView = _defaultFoV;
        }
    }
}