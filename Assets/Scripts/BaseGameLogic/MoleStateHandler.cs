using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MoleStateHandler : MonoBehaviour
{
    [SerializeField] MoleSpawner _moleSpawner;

    private List<Mole> _currentMoles = new List<Mole>();

    public event UnityAction MoleEscaped;
    public event UnityAction MoleKilled;

    private void OnEnable()
    {
        _moleSpawner.MoleSetted += OnMoleSetted;
    }

    private void OnDisable()
    {
        _moleSpawner.MoleSetted -= OnMoleSetted;
        UnSubscribeToMoles();
    }

    private void UnSubscribeToMoles()
    {
        foreach (var mole in _currentMoles)
        {
            mole.MoleEscaped -= OnMoleEscaped;
            mole.MoleKilled -= OnMoleKilled;
        }
    }

    private void SubscribeToMoles(Mole mole)
    {
        mole.MoleEscaped += OnMoleEscaped;
        mole.MoleKilled += OnMoleKilled;
    }

    private void OnMoleSetted(Mole mole)
    {
        _currentMoles.Add(mole);
        SubscribeToMoles(_currentMoles.Last());
    }

    private void OnMoleEscaped(Mole mole)
    {
        MoleEscaped?.Invoke();

        mole.MoleEscaped -= OnMoleEscaped;
        mole.MoleKilled -= OnMoleKilled;

        _currentMoles.Remove(mole);
    }

    private void OnMoleKilled(Mole mole)
    {
        MoleKilled?.Invoke();

        mole.MoleEscaped -= OnMoleEscaped;
        mole.MoleKilled -= OnMoleKilled;

        _currentMoles.Remove(mole);
    }
}
