using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int enemyCountOnScene;
    [SerializeField]
    private int enemyStartCount;
    [SerializeField]
    private float enemySpawnDelay;
    [SerializeField]
    private float enemySpawnDelayDelta;
    [SerializeField]
    private int enemyCountDelta;
    [SerializeField]
    private float enemyBaseSpeed;
    [SerializeField]
    private float enemySpeedDelta;


    private SpawnPoints _spawnPoints;
    private int currentEnemyCount = 0;
    private DiContainer _diContainer;
    private int _currentWaveIndex = 1;
    private int _currentWaveEnemyCount;
    private float _currentEnemySpawnDelay;
    private int _enemyKilledOnCurrentWave;
    private float _currentEnemySpeed;

    public Action<int> OnWaveChangingEvent;

    [Inject]
    public void Construct(SpawnPoints spawnPoints,DiContainer diContainer)
    {
        _spawnPoints = spawnPoints;
        _diContainer = diContainer;
    }

    private IEnumerator TryToSpawnEnemy(GameObject enemyPrefab,float delay)
    {
        if (currentEnemyCount < enemyCountOnScene && _spawnPoints != null)
            SpawnEnemy(enemyPrefab);
        yield return new WaitForSeconds(delay);
        StartCoroutine(TryToSpawnEnemy(enemyPrefab, _currentEnemySpawnDelay));
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        var randomSpawnPointIndex = UnityEngine.Random.Range(0, _spawnPoints.Points.Count);
        var enemy = _diContainer.InstantiatePrefabForComponent<EnemyController>(enemyPrefab, _spawnPoints.Points[randomSpawnPointIndex].transform.position, Quaternion.identity, null);
        enemy.GetComponent<NavMeshAgent>().speed = _currentEnemySpeed;
        enemy.OnEnemyDieEvent += EnemyKilledEventHandler;
        currentEnemyCount++;
    }

    private void TryToChangeWave(int currentEnemyKilled, int currentWaveEnemyCount)
    {
        if (currentEnemyKilled >= currentWaveEnemyCount)
        {
            _currentWaveIndex++;
            ChangeWaveByIndex(_currentWaveIndex);
        }

    }

    private void ChangeWaveByIndex(int waveIndex)
    {
        OnWaveChangingEvent?.Invoke(waveIndex);
        _currentEnemySpeed = enemySpeedDelta * waveIndex + enemyBaseSpeed;
        _currentWaveEnemyCount = enemyCountDelta * waveIndex + enemyStartCount;
        _currentEnemySpawnDelay = enemySpawnDelayDelta * waveIndex + enemySpawnDelayDelta;

    }

    private void EnemyKilledEventHandler(EnemyController enemyController)
    {
        enemyController.OnEnemyDieEvent -= EnemyKilledEventHandler;
        _enemyKilledOnCurrentWave++;
        TryToChangeWave(_enemyKilledOnCurrentWave, _currentWaveEnemyCount);
    }

    private void Awake()
    {
        ChangeWaveByIndex(_currentWaveIndex);
        _enemyKilledOnCurrentWave = 0;
        StartCoroutine(TryToSpawnEnemy(enemyPrefab, enemySpawnDelay));
    }
}
