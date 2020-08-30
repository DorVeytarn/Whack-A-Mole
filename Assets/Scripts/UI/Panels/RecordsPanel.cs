using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordsPanel : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Animator _animator;
    [SerializeField] private ScoreHandler _scoreHandler;

    private int _open = Animator.StringToHash("Open");
    private int _close = Animator.StringToHash("Close");

    private void OnEnable()
    {
        _scoreHandler.HightScoreChanged += OnHightScoreChanged;
        _backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnDisable()
    {
        _scoreHandler.HightScoreChanged -= OnHightScoreChanged;
        _backButton.onClick.RemoveListener(OnBackButtonClick);
    }

    private void Start()
    {
        _scoreText.text = Loader.LoadScore("Score").ToString();
    }

    private void OnHightScoreChanged(int scoreValue)
    {
        _scoreText.text = scoreValue.ToString();
    }

    public void OpenPanel()
    {
        _animator.SetTrigger(_open);
    } 

    private void OnBackButtonClick()
    {
        _animator.SetTrigger(_close);
    }
}
