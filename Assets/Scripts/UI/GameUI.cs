using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviour
{
    public EnemySpawner EnemySpawner { get; private set; }
    public PlayerData Player { get; private set; }
    [Inject]
    public void Construct(EnemySpawner enemySpawner,PlayerData player)
    {
        EnemySpawner = enemySpawner;
        Player = player;
    }
}
