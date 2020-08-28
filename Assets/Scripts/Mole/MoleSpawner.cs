using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : ObjectPool
{
    [SerializeField] private GameObject _molePrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _timeBetweenSpawns;

    private float _elapsedTime = 0;

    private void Start()
    {
        MoleSpawnPoint[] points = GetComponentsInChildren<MoleSpawnPoint>();
        Debug.Log(points.Length);
        _spawnPoints = new Transform[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            _spawnPoints[i] = points[i].transform;
        }
        Initialize(_molePrefab);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _timeBetweenSpawns)
        {
            if(TryGetObject(out GameObject mole))
            {
                _elapsedTime = 0;

                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);

                SetMole(mole, _spawnPoints[spawnPointNumber]);
            }
        }
    }

    private void SetMole(GameObject mole, Transform parent)
    {
        mole.SetActive(true);
        mole.transform.SetParent(parent, false);
    }
}
