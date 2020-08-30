using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Mole : MonoBehaviour, IPointerDownHandler
{
    [Header("Base Properties")]
    [SerializeField] private int _health;
    [SerializeField] private float _lifetime;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _climbingSpeed;

    [Header("Time Dependece")]
    [SerializeField] private AnimationCurve _moleSpeedMultiplier;
    [SerializeField] private AnimationCurve _moleLifetimeMultiplier;
    [SerializeField] private AnimationCurve _moleAdditionalHealth;

    private float _currentTime;
    private float _elapsedTime;
    private WaitForSeconds _oneSecond = new WaitForSeconds(1f);

    public event UnityAction<Mole> MoleKilled;
    public event UnityAction<Mole> MoleEscaped;

    private void OnEnable()
    {
        _animator.speed = _climbingSpeed;

        StartCoroutine(TimeDown());
        StartCoroutine(ChangeProperties());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ApplyDamage();
    }

    private void ApplyDamage()
    {

        _health--;
        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        

        gameObject.SetActive(false);
        StopCoroutine(TimeDown());

        MoleKilled?.Invoke(this);
    }
    private void Escape()
    {
        _animator.SetTrigger("isTimeOut");
        Invoke(nameof(DisableMole), 0.1f);
        StopCoroutine(TimeDown());

        MoleEscaped?.Invoke(this);
    }

    private void DisableMole()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator TimeDown()
    {
        _currentTime = _lifetime;
        while (true)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
                Escape();

            yield return null;
        }
    }

    private IEnumerator ChangeProperties()
    {
        while (true)
        {
            _elapsedTime++; 

            _health = Mathf.RoundToInt(_moleAdditionalHealth.Evaluate(_elapsedTime));
            _animator.speed = _moleSpeedMultiplier.Evaluate(_elapsedTime);
            _climbingSpeed = _animator.speed;
            _lifetime = _moleLifetimeMultiplier.Evaluate(_elapsedTime);

            yield return _oneSecond;
        }
    }
}
