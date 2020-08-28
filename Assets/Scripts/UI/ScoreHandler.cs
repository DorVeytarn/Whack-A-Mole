using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _hightScoreText;
    [SerializeField] private MoleStateHandler _stateHandler;

    private int _currentScore;
    private int _hightScore;

    private void OnEnable()
    {
        _stateHandler.MoleKilled += OnMoleKilled;

        _hightScoreText.text = "Рекорд: " + _hightScore.ToString();
        _scoreText.text = "Текущий счёт: " + _currentScore.ToString();
    }

    private void OnDisable()
    {
        _stateHandler.MoleKilled -= OnMoleKilled;
    }

    private void OnMoleKilled()
    {
        _currentScore++;
        _hightScore = _currentScore;

        _scoreText.text = "Текущий счёт: " + _currentScore.ToString();
        _hightScoreText.text = "Рекорд: " + _hightScore.ToString();
    }
}
