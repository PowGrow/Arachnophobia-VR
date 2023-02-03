using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviour
{
    public EnemySpawner EnemySpawner { get; private set; }
    [Inject]
    public void Construct(EnemySpawner enemySpawner)
    {
        EnemySpawner = enemySpawner;
    }
}
