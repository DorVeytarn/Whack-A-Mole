using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _hightScoreText;
    [SerializeField] private MoleStateHandler _stateHandler;

    private int _currentScore;
    private int _hightScore;

    public event UnityAction<int> HightScoreChanged;

    private void OnEnable()
    {
        _hightScore = Loader.LoadScore("Score");

        _stateHandler.MoleKilled += OnMoleKilled;

        _hightScoreText.text = "Рекорд: " + _hightScore.ToString();
        _scoreText.text = "Текущий счёт: " + _currentScore.ToString();
    }

    private void OnDisable()
    {
        _stateHandler.MoleKilled -= OnMoleKilled;
    }

    private void OnMoleKilled(Mole mole)
    {
        if (mole.TryGetComponent(out GoldMole goldMole))
        {
            _currentScore += 5;
        }
        else
            _currentScore++;

        if(_hightScore <= _currentScore)
        {
            _hightScore = _currentScore;
            Saver.SaveScore("Score", _hightScore);
            HightScoreChanged?.Invoke(_hightScore);
        }

        _scoreText.text = "Текущий счёт: " + _currentScore.ToString();
        _hightScoreText.text = "Рекорд: " + _hightScore.ToString();
    }

    private void OnApplicationQuit()
    {
        Saver.SaveScore("Score", _hightScore);
    }
}
