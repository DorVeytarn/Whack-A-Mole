using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _recordButton;
    [SerializeField] private Button _moleListButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Animator _animator;
    [SerializeField] private RecordsPanel _recordsPanel;
    [SerializeField] private MoleListPanel _moleListPanel;

    private int _open = Animator.StringToHash("Open");
    private int _close = Animator.StringToHash("Close");

    public event UnityAction GameStarted;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _recordButton.onClick.AddListener(OnRecordButtonClick);
        _moleListButton.onClick.AddListener(OnMoleListButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _recordButton.onClick.RemoveListener(OnRecordButtonClick);
        _moleListButton.onClick.RemoveListener(OnMoleListButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnPlayButtonClick()
    {
        _animator.SetTrigger(_close);
        GameStarted?.Invoke();
    }

    private void OnRecordButtonClick()
    {
        _recordsPanel.OpenPanel();
    }

    private void OnMoleListButtonClick()
    {
        _moleListPanel.OpenPanel();
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
