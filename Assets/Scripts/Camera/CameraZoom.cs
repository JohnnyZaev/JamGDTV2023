using UnityEngine;
using UnityEngine.Rendering;

public class CameraZoom : MonoBehaviour
{
    public GameObject targetObject;
    [SerializeField] private float minimumZoom;
    [SerializeField] private float maximumZoom;
    [SerializeField] private Volume blur;
    [SerializeField] private Camera blurCamera;
    private bool _zoomStarted = false;
    private bool _zoomOutStarted = false;
    private float _zoom;
    private float _zoomMultiplier = 1f;
    private float _velocity = 0f;
    private float _smoothTime = 0.1f;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        _zoom = _camera.orthographicSize;
    }
    void Update()
    {
        if (_zoomStarted)
        {
            transform.position = Vector3.Lerp(transform.position, targetObject.transform.position, 4f * Time.deltaTime);
            ZoomToTarget(0.1f);
        }
        if (_zoomOutStarted)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), 4f * Time.deltaTime);
            ZoomToTarget(-0.1f);
        }
        if (transform.position == new Vector3(0, 0, 0))
        {
            _zoomStarted = false;
            _zoomOutStarted = false;
        }
    }

    public void StartZooming()
    {
        targetObject.layer = 7;
        blur.gameObject.SetActive(true);
        _zoomStarted = true;
        _zoomOutStarted = false;
    }

    public void ZoomOutStart()
    {
        targetObject.layer = 6;
        blur.gameObject.SetActive(false);
        _zoomOutStarted = true;
        _zoomStarted = false;
    }
    public void ZoomToTarget(float scroll)
    {
        if (_zoom >= minimumZoom)
        {
            _zoom -= scroll * _zoomMultiplier;
            _zoom = Mathf.Clamp(_zoom, minimumZoom, maximumZoom);
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, _zoom, ref _velocity, _smoothTime);
            blurCamera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, _zoom, ref _velocity, _smoothTime);
        }
    }
}
