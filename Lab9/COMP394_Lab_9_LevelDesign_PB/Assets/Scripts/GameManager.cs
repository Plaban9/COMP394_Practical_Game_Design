using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _totalPickups;
    [SerializeField] private int _currentPickups;
    [SerializeField] private float _maxTimer;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _pickupText;

    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;

    Timer _timer;

    public static GameManager Instance
    {
        get; private set;       
    }

    private void Awake()
    {
        _currentPickups = 0;

        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);

        _timer = new Timer(_maxTimer);
        _timer.StartTimer();
        _timer.OnTimerEnd += LoseCondition;

        Instance = this;

        _pickupText.text = $"PICKUPS: {_currentPickups} / {_totalPickups}";
    }

    private void Update()
    {
        _timer.UpdateTimer();
        if (_timerText != null)
        {
            _timerText.text = $"TIMER: {_timer.GetDisplayTimer()}";
        }
    }

    public void IncrementPickups()
    {
        _currentPickups += 1;
        _pickupText.text = $"PICKUPS: {_currentPickups} / {_totalPickups}";

        if (_currentPickups >= _totalPickups)
        {
            WinCondition();
        }
    }

    private void WinCondition()
    {
        if (_timer.HAS_ENDED)
        {
            _timer.PauseTimer();
            LoseCondition();
            return;
        }

        Debug.Log("You Won!!");

        _winScreen.SetActive(true);
    }

    private void LoseCondition()
    {
        Debug.Log("You Lose!!");

        _loseScreen.SetActive(true);
    }
}
