using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace CameraController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Volume blur;
        [SerializeField] private Camera blurCamera;
        private float _zoomSpeed = 1f;
        private float cameraSpeed = 4f;
        private Camera _camera;
        private float _targretZoom;
        private Vector3 _targretPosition;

        void Start()
        {
            _camera = Camera.main;
            _targretPosition = transform.position;
            _targretZoom = _camera.orthographicSize;
        }
        void Update()
        {
            if (transform.position != _targretPosition)
            {
                transform.position = Vector3.Lerp(transform.position, _targretPosition, cameraSpeed * Time.deltaTime);
            }
            if (_camera.orthographicSize != _targretZoom)
            {
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targretZoom, _zoomSpeed * Time.deltaTime);
                blurCamera.orthographicSize = _camera.orthographicSize;
            }
        }

        public void Zoom(float zoom, float speed)
        {
            _targretZoom = zoom;
            _zoomSpeed = speed;
        }

        public void Move(Vector3 position)
        {
            _targretPosition = position;
        }

        public void Blur(bool isActive)
        {
            blur.gameObject.SetActive(isActive);
        }
    }
}
