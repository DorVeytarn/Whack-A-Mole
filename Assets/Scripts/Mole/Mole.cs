using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Mole : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _lifetime;
    [SerializeField] protected int _reward;
    [SerializeField] protected Animator _animator;

    protected float _currentTime;

    public event UnityAction<Mole> MoleKilled;
    public event UnityAction<Mole> MoleEscaped;

    private void OnEnable()
    {
        StartCoroutine(TimeDown());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ApplyDamage();
    }

    protected void ApplyDamage()
    {
        _health--;
        if (_health <= 0)
            Die();
    }

    protected void Die()
    {
        gameObject.SetActive(false);
        StopCoroutine(TimeDown());

        MoleKilled?.Invoke(this);
    }
    protected void Escape()
    {
        _animator.SetTrigger("isTimeOut");
        Invoke(nameof(DisableMole), 0.1f);
        StopCoroutine(TimeDown());

        MoleEscaped?.Invoke(this);
    }

    protected void DisableMole()
    {
        gameObject.SetActive(false);
    }

    protected IEnumerator TimeDown()
    {
        _currentTime = _lifetime;
        while (true)
        {
            _currentTime--;
            yield return new WaitForSeconds(1f);

            if (_currentTime <= 0)
                Escape();
        }
    }
}
