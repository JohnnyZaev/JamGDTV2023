using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GameInfo
{
    public class GameInfo : MonoBehaviour
    {
        // Added new variable, due to inability of properties to show up in Editor, so property is linked to the variable from the editor
        [HideInInspector] public int levelsCompleted = 0;
        [SerializeField] private TextMeshProUGUI timerTextField;
        [SerializeField] private float timerStart;
        [SerializeField] private float goodEndingSparkles;
        [SerializeField] private int maxLevels;
        [SerializeField] private UnityEvent onEndGame;
        [SerializeField] private UnityEvent startGame;
        private bool _isTimerStarted = false;
        private int _sparklesCounter;
        public float TimerStart { get => timerStart;
            private set => timerStart = value;
        }
        public int SparklesCounter
        {
            get => _sparklesCounter;
            set => _sparklesCounter += 1;
        }

        private void Start()
        {
            //StartTimer(); // For debug
            DialogueController.Instance.StartFirstDialogue(DialogueController.Instance.starterDialogue, startGame);
        }

        private void Update()
        {
            if (!_isTimerStarted) return;
            TimerStart -= Time.deltaTime;
            timerTextField.text = $"{Mathf.FloorToInt(TimerStart) / 60} : {Mathf.FloorToInt(TimerStart) % 60}";
        }

        public void StartTimer()
        {
            _isTimerStarted = true;
        }

        public void StopTimer()
        {
            _isTimerStarted = false;
        }

        public void SparklesAdded()
        {
            SparklesCounter++;
        }

        public void LevelCompleted()
        {
            levelsCompleted++;

            if (levelsCompleted == maxLevels)
            {
                onEndGame.Invoke();
            }
        }
    }
}
