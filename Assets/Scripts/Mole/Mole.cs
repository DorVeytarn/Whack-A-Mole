using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Mole : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _lifetime;
    [SerializeField] protected int _reward;
    [SerializeField] protected Animator _animator;

    protected float _currentTime;

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
    }
    protected void Escape()
    {
        _animator.SetTrigger("isTimeOut");
        Invoke(nameof(Die), 1f);
    }

    protected IEnumerator TimeDown()
    {
        _currentTime = _lifetime;
        while (true)
        {
            _currentTime--;
            yield return new WaitForSeconds(1f);
        }
    }
}
