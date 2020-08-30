using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleListPanel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Button _backButton;

    private int _open = Animator.StringToHash("Open");
    private int _close = Animator.StringToHash("Close");

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnBackButtonClick);
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
