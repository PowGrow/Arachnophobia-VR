using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform startPosition;
    [SerializeField]
    private GameObject startGameUi;

    public override void InstallBindings()
    {
        var player = Container.InstantiatePrefabForComponent<PlayerData>(playerPrefab, startPosition.position, Quaternion.identity, null);
        Container.Bind<PlayerData>().FromInstance(player).AsSingle();

        Container.InstantiatePrefabForComponent<MainMenuUi>(startGameUi);
    }
}
