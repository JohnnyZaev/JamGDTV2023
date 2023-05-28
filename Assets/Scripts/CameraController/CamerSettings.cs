using System.Collections;
using UnityEngine;

namespace CameraController
{
    [CreateAssetMenu(menuName = "CameraSettings")]
    public class CamerSettings : ScriptableObject
    {
        public float zoom;
        public float zoomSpeed;
    }
}
