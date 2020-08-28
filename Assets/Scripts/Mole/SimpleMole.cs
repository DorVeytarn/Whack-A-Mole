using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleMole : Mole, IPointerDownHandler
{
    private void OnEnable()
    {
        StartCoroutine(TimeDown());
    }

    private void Update()
    {
        if (_currentTime <= 0)
            Escape();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        ApplyDamage();
    }
}
