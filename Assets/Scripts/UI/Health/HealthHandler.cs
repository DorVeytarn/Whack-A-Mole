using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private MoleStateHandler _stateHandler;

    private int _currentHealth;

    public event UnityAction GameOvered;
    public event UnityAction<int> HealthChanged;

    private void OnEnable()
    {
        _stateHandler.MoleEscaped += OnMoleEscaped;
        _stateHandler.MoleKilled += OnMoleKilled;
    }

    private void OnDisable()
    {
        _stateHandler.MoleEscaped -= OnMoleEscaped;
        _stateHandler.MoleKilled -= OnMoleKilled;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealth();
    }

    private void OnMoleEscaped(Mole mole)
    {
        if (mole.TryGetComponent(out BombMole bombMole))
        {
            Debug.Log("BombMole");
        }
        else if (mole.TryGetComponent(out HeartMole heartMole))
        {
            Debug.Log("HeartMole");
        }
        else
            _currentHealth--;
        UpdateHealth();
    }

    private void OnMoleKilled(Mole mole)
    {
        if (mole.TryGetComponent(out BombMole bombMole))
        {
            _currentHealth -= 2;
            Debug.Log("BombMole");
        }
        else if (mole.TryGetComponent(out HeartMole heartMole))
        {
            _currentHealth++;
            Debug.Log("HeartMole");
        }

        UpdateHealth();
    }

    private void UpdateHealth()
    {
        HealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        GameOvered?.Invoke();
        _stateHandler.MoleEscaped -= OnMoleEscaped;
    }
}
