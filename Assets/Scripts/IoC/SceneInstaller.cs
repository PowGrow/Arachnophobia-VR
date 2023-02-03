using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject pistolPrefab;
    [SerializeField]
    private GameObject spawnPointsPrefab;
    [SerializeField]
    private GameObject enemySpawnerPrefab;
    [SerializeField]
    private GameObject gameUiPrefab;
    [SerializeField]
    private Transform pistolPosition;

    public override void InstallBindings()
    {
        PistolBase pistol = Container.InstantiatePrefabForComponent<PistolBase>(pistolPrefab, pistolPosition.position, pistolPosition.rotation, null);
        SpawnPoints spawnPoints = Container.InstantiatePrefabForComponent<SpawnPoints>(spawnPointsPrefab, Vector3.zero, Quaternion.identity, null);
        Container.Bind<SpawnPoints>().FromInstance(spawnPoints).AsSingle();

        EnemySpawner enemySpawner = Container.InstantiatePrefabForComponent<EnemySpawner>(enemySpawnerPrefab, Vector3.zero, Quaternion.identity, null);
        Container.Bind<EnemySpawner>().FromInstance(enemySpawner).AsSingle();

        GameUI gameUI = Container.InstantiatePrefabForComponent<GameUI>(gameUiPrefab, Vector3.zero, Quaternion.identity, null);

    }
}