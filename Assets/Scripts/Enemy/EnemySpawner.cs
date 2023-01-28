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

    [Inject]
    public void Construct(SpawnPoints spawnPoints)
    {
        _spawnPoints = spawnPoints;
    }

    private IEnumerator TryToSpawnEnemy(GameObject enemyPrefab,float delay)
    {
        if (currentEnemyCount < enemyCountOnScene && _spawnPoints != null)
            SpawnEnemy(enemyPrefab);
        yield return new WaitForSeconds(delay);
        StartCoroutine(TryToSpawnEnemy(enemyPrefab, enemySpawnDelay));
    }

    private GameObject SpawnEnemy(GameObject enemyPrefab)
    {
        var randomSpawnPointIndex = Random.Range(0, _spawnPoints.Points.Count);
        return Instantiate(enemyPrefab, _spawnPoints.Points[randomSpawnPointIndex].transform.position, Quaternion.identity);
    }

    private void Awake()
    {
        StartCoroutine(TryToSpawnEnemy(enemyPrefab, enemySpawnDelay));
    }
}
