using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoleSpawner : ObjectPool
{
    [Header("Base Properties")]
    [SerializeField] private GameObject[] _molePrefabs;
    [SerializeField] private float _defaultTimeBetweenSpawns;
    [SerializeField] private bool _canSpawn = false;

    [Header("Game Logic")]
    [SerializeField] private MainPanel _mainPanel;

    [Header("Time Dependece")]
    [SerializeField] private AnimationCurve _timeBetweenSpawns;

    private List<Transform> _spawnPoints = new List<Transform>();

    private float _elapsedTime = 0;
    private float _elapsedTimeForCoroutine;
    private WaitForSeconds _oneSecond = new WaitForSeconds(1f);
    
    public event UnityAction<Mole> MoleSetted;

    private void OnEnable()
    {
        _mainPanel.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        _mainPanel.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        _canSpawn = true;
        StartCoroutine(ChangeTimeBetweenSpawn());
    }

    private void Start()
    {
        GetSpawnPoints();
        Initialize(_molePrefabs);
    }

    private void Update()
    {
        if (_canSpawn)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _timeBetweenSpawns.Evaluate(_elapsedTimeForCoroutine))
            {
                if(TryGetObject(out GameObject mole))
                {
                    _elapsedTime = 0;

                    int spawnPointNumber = Random.Range(0, _spawnPoints.Count);
                    if (_spawnPoints[spawnPointNumber] == null)
                        return;

                    SetMole(mole, _spawnPoints[spawnPointNumber]);
                    TemporarilyRemovePoint(spawnPointNumber, _timeBetweenSpawns.Evaluate(_elapsedTimeForCoroutine));
                }
            }
        }
    }

    private void GetSpawnPoints()
    {
        MoleSpawnPoint[] points = GetComponentsInChildren<MoleSpawnPoint>();
        for (int i = 0; i < points.Length; i++)
        {
            _spawnPoints.Add(points[i].transform);
        }
    }

    private void TemporarilyRemovePoint(int pointIndex, float timeValue)
    {
        var deletedPoint = _spawnPoints[pointIndex];
        _spawnPoints.Remove(deletedPoint);
        StartCoroutine(ReturnPoint(deletedPoint, timeValue));
    }


    private void SetMole(GameObject moleTemplate, Transform parent)
    {
        moleTemplate.SetActive(true);
        moleTemplate.transform.SetParent(parent, false);

        if (moleTemplate.TryGetComponent(out Mole mole))
            MoleSetted?.Invoke(mole);
    }

    private IEnumerator ReturnPoint(Transform pointTorRerutn, float timeValue)
    {
        yield return new WaitForSeconds(timeValue);
        _spawnPoints.Add(pointTorRerutn);
        StopCoroutine(nameof(ReturnPoint));
    }

    private IEnumerator ChangeTimeBetweenSpawn()
    {
        while (true)
        {
            _elapsedTimeForCoroutine++;

            yield return _oneSecond;
        }
    }
}
