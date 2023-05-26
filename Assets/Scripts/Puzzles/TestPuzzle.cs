using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzles
{
    public class TestPuzzle : MonoBehaviour, IPuzzle
    {
        public UnityEvent OnSuccess { get; set; }
        public UnityEvent OnFailure { get; set; }

        // Start is called before the first frame update
        void Start()
        {

        }

        void IPuzzle.Start()
        {
            OnSuccess.Invoke();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
