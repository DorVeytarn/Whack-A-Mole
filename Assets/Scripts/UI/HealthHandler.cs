using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Text _healthText;
    [SerializeField] private MoleStateHandler _stateHandler;

    private int _currentHealth;

    private void OnEnable()
    {
        _stateHandler.MoleEscaped += OnMoleEscaped;
        _currentHealth = _maxHealth;

        _healthText.text = "Жизней: " + _currentHealth.ToString();
    }

    private void OnDisable()
    {
        _stateHandler.MoleKilled -= OnMoleEscaped;
    }

    private void OnMoleEscaped()
    {
        _currentHealth--;
        _healthText.text = "Жизней: " + _currentHealth.ToString();
    }

}
