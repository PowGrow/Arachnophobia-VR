using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using System;
using Zenject.SpaceFighter;
using System.Collections.Generic;

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
    [SerializeField]
    private float enemyDamageDelta;
    [SerializeField]
    private float enemyHealthDelta;

    private DiContainer _diContainer;
    private SpawnPoints _spawnPoints;
    private int _currentEnemyCount = 0;
    private int _currentWaveIndex = 1;
    private int _currentWaveEnemyCount;
    private float _currentEnemySpawnDelay;
    private int _enemyKilledOnCurrentWave;
    private float _currentEnemySpeed;
    private float _currentEnemyDamageDelta;
    private float _curerentEnemyHealthDelta;
    private bool _isGameOver = false;
    private PlayerData _player;
    private List<EnemyController> ActiveEnemies;

    public event Action<int> OnWaveChangingEvent;
    public event Action OnEnemyKilledEvent;
    [Inject]
    public void Construct(SpawnPoints spawnPoints,DiContainer diContainer,PlayerData player)
    {
        _spawnPoints = spawnPoints;
        _diContainer = diContainer;
        _player = player;
        
    }

    private void StopSpawnAndKillEnemies()
    {
        _isGameOver = true;
        KillAllActiveEnemies();
        _player.OnPlayerDieEvent -= StopSpawnAndKillEnemies;
    }

    private IEnumerator TryToSpawnEnemy(GameObject enemyPrefab,float delay)
    {
        if (_isGameOver)
            yield return null;
        if (_currentEnemyCount < enemyCountOnScene && _spawnPoints != null)
            SpawnEnemy(enemyPrefab);
        yield return new WaitForSeconds(delay);
        StartCoroutine(TryToSpawnEnemy(enemyPrefab, _currentEnemySpawnDelay));
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        var randomSpawnPointIndex = UnityEngine.Random.Range(0, _spawnPoints.Points.Count);
        var enemy = _diContainer.InstantiatePrefabForComponent<EnemyController>(enemyPrefab, _spawnPoints.Points[randomSpawnPointIndex].transform.position, Quaternion.identity, null);
        ActiveEnemies.Add(enemy);
        enemy.GetComponent<NavMeshAgent>().speed = _currentEnemySpeed;
        enemy.Mutate(enemyDamageDelta, enemyHealthDelta);
        enemy.OnEnemyDieEvent += EnemyKilledEventHandler;
        _currentEnemyCount++;
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
        _currentEnemyDamageDelta = enemyDamageDelta * waveIndex;
        _curerentEnemyHealthDelta = enemyHealthDelta * waveIndex;
        _enemyKilledOnCurrentWave = 0;

    }

    private void KillAllActiveEnemies()
    {
        for(int i = ActiveEnemies.Count - 1; i >= 0; i--)
        {
            ActiveEnemies[i].Kill();
        }
    }

    private void EnemyKilledEventHandler(EnemyController enemyController)
    {
        if(!_isGameOver)
        {
            _currentEnemyCount--;
            OnEnemyKilledEvent?.Invoke();
            _enemyKilledOnCurrentWave++;
            TryToChangeWave(_enemyKilledOnCurrentWave, _currentWaveEnemyCount);
        }
        ActiveEnemies.Remove(enemyController);
        enemyController.OnEnemyDieEvent -= EnemyKilledEventHandler;
    }

    private void Awake()
    {
        ActiveEnemies = new List<EnemyController>();
        _player.OnPlayerDieEvent += StopSpawnAndKillEnemies;
        ChangeWaveByIndex(_currentWaveIndex);
        _enemyKilledOnCurrentWave = 0;
        StartCoroutine(TryToSpawnEnemy(enemyPrefab, enemySpawnDelay));
    }
}
