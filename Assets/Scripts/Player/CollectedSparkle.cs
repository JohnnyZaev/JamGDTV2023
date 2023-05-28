using System;
using UnityEngine;

namespace Player
{
    public class CollectedSparkle : MonoBehaviour
    {
        public float rotationSpeed = 90.0f;
        [HideInInspector] public float currentRotationSpeed;

        private void Awake()
        {
            currentRotationSpeed = rotationSpeed;
        }

        private void Update()
        {
            transform.Rotate(0f, currentRotationSpeed * Time.deltaTime, 0f);
        }
    }
}