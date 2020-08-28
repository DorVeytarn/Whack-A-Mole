using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameObject _parentHoleColumn;
    [SerializeField] private GameObject _rawTemplate;
    [SerializeField] private GameObject _holeTemplate;

    private int _rawAmount;
    private int _currentHoleAmount;

    private void Awake()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        _rawAmount = _levelManager.HoleAmountInRaw.Length;

        for (int i = 0; i < _rawAmount; i++)
        {
            var newRaw = Instantiate(_rawTemplate, _parentHoleColumn.transform);
            _currentHoleAmount = _levelManager.HoleAmountInRaw[i];
            for (int j = 0; j < _currentHoleAmount; j++)
            {
                var newHole = Instantiate(_holeTemplate, newRaw.transform);
            }
        }
    }

}
