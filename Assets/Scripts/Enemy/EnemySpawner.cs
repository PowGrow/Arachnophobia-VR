using System.Collections;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int enemyCountOnScene;
    [SerializeField]
    private float enemySpawnDelay;


    private SpawnPoints _spawnPoints;
    private int currentEnemyCount = 0;
    private DiContainer _diContainer;

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
        StartCoroutine(TryToSpawnEnemy(enemyPrefab, enemySpawnDelay));
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        var randomSpawnPointIndex = Random.Range(0, _spawnPoints.Points.Count);
        _diContainer.InstantiatePrefabForComponent<EnemyController>(enemyPrefab, _spawnPoints.Points[randomSpawnPointIndex].transform.position, Quaternion.identity, null);
    }

    private void Awake()
    {
        StartCoroutine(TryToSpawnEnemy(enemyPrefab, enemySpawnDelay));
    }
}
