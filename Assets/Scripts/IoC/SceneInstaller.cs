using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _startPosition;

    public override void InstallBindings()
    {
        PlayerData playerData = Container.InstantiatePrefabForComponent<PlayerData>(_player,_startPosition.position, _startPosition.rotation, null);
        Container.Bind<PlayerData>().FromInstance(playerData).AsSingle();
    }
}