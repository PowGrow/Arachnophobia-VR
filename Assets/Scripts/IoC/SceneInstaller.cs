using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform startPosition;
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
    [SerializeField]
    private GameObject inGameMusicPrefab;
    [SerializeField]
    private GameObject weaponSpawnerPrefab;

    public override void InstallBindings()
    {
        var player = Container.InstantiatePrefabForComponent<PlayerData>(playerPrefab, startPosition.position, Quaternion.identity, null);
        Container.Bind<PlayerData>().FromInstance(player).AsSingle();

        Container.InstantiatePrefab(pistolPrefab, pistolPosition.position, pistolPosition.rotation, null);
        SpawnPoints spawnPoints = Container.InstantiatePrefabForComponent<SpawnPoints>(spawnPointsPrefab, Vector3.zero, Quaternion.identity, null);
        Container.Bind<SpawnPoints>().FromInstance(spawnPoints).AsSingle();

        EnemySpawner enemySpawner = Container.InstantiatePrefabForComponent<EnemySpawner>(enemySpawnerPrefab, Vector3.zero, Quaternion.identity, null);
        Container.Bind<EnemySpawner>().FromInstance(enemySpawner).AsSingle();

        GameUI gameUI = Container.InstantiatePrefabForComponent<GameUI>(gameUiPrefab, Vector3.zero, Quaternion.identity, null);
        Container.Bind<GameUI>().FromInstance(gameUI).AsSingle();
        Container.InstantiatePrefabForComponent<GameOverSoundProvider>(inGameMusicPrefab);
        Container.InstantiatePrefabForComponent<WeaponSpawner>(weaponSpawnerPrefab);
    }
}