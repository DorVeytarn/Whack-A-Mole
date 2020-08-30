using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Button _replayButton;
    [SerializeField] private Animator _animator;
    [SerializeField] private HealthHandler _healthHandler;

    private int _open = Animator.StringToHash("Open");
    private int _close = Animator.StringToHash("Close");

    private void OnEnable()
    {
        _healthHandler.GameOvered += OnGameOvered;
        _replayButton.onClick.AddListener(OnReplayButtonClick);
    }

    private void OnDisable()
    {
        _healthHandler.GameOvered -= OnGameOvered;
        _replayButton.onClick.RemoveListener(OnReplayButtonClick);
    }

    private void OnGameOvered()
    {
        _animator.SetTrigger(_open);
    }

    private void OnReplayButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
