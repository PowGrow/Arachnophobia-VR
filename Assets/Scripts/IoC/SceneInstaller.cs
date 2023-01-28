using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject pistolPrefab;
    [SerializeField]
    private GameObject spawnPointsPrefab;
    [SerializeField]
    private GameObject enemySpawnerPrefab;
    [SerializeField]
    private Transform startPosition;
    [SerializeField]
    private Transform pistolPosition;

    public override void InstallBindings()
    {
        PlayerData playerData = Container.InstantiatePrefabForComponent<PlayerData>(playerPrefab,startPosition.position, startPosition.rotation, null);
        PistolBase pistol = Container.InstantiatePrefabForComponent<PistolBase>(pistolPrefab, pistolPosition.position, pistolPosition.rotation, null);
        SpawnPoints spawnPoints = Container.InstantiatePrefabForComponent<SpawnPoints>(spawnPointsPrefab, Vector3.zero, Quaternion.identity, null);

        Container.Bind<PlayerData>().FromInstance(playerData).AsSingle();
        Container.Bind<SpawnPoints>().FromInstance(spawnPoints).AsSingle();

        EnemySpawner enemySpawner = Container.InstantiatePrefabForComponent<EnemySpawner>(enemySpawnerPrefab, Vector3.zero, Quaternion.identity, null);
    }
}