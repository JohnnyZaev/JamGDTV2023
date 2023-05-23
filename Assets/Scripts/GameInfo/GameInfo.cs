using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameInfo : MonoBehaviour
{
    // Added new variable, due to inability of properties to show up in Editor, so property is linked to the variable from the editor
    [SerializeField] private TextMeshProUGUI timerTextField; 
    [SerializeField] private float timerStart;
    [SerializeField] private int maxSparkles;
    [SerializeField] private float goodEndingSparkles;
    [SerializeField] private UnityEvent allSparklesCollected;
    private bool _isTimerStarted = false;
    private int _sparklesCounter;

    private TextMeshProUGUI TimerTextField { get { return timerTextField; } }
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
            timerStart -= Time.deltaTime;
            TimerTextField.text = Mathf.Round(timerStart).ToString();
        }
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
        if (SparklesCounter != maxSparkles)
        {
            SparklesCounter++;
        }

        if (SparklesCounter == maxSparkles)
        {
            allSparklesCollected.Invoke();
        }
    }
}
