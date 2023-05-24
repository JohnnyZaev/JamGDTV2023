using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameInfo : MonoBehaviour
{
    // Added new variable, due to inability of properties to show up in Editor, so property is linked to the variable from the editor
    [HideInInspector]
    public int levelsCompleted = 0;
    [SerializeField] private TextMeshProUGUI timerTextField;
    [SerializeField] private float timerStart;
    [SerializeField] private float goodEndingSparkles;
    [SerializeField] private UnityEvent onEndGame;
    [SerializeField] private int maxLevels;
    private bool _isTimerStarted = false;
    private int _sparklesCounter;
    public float TimerStart { get { return timerStart; } private set { TimerStart = value; } }
    public int SparklesCounter
    {
        get
        {
            return _sparklesCounter; 
        }
        set
        {
            _sparklesCounter += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTimerStarted)
        {
            TimerStart -= Time.deltaTime;
            timerTextField.text = Mathf.Round(timerStart).ToString();
        }
    }

    #region Start/Stop Timer functions
    public void StartTimer()
    {
        _isTimerStarted = true;
    }

    public void StopTimer()
    {
        _isTimerStarted = false;
    }

    #endregion
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
