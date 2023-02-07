using UnityEngine;
using Zenject;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _anounceTextObject;
    public EnemySpawner EnemySpawner { get; private set; }
    public PlayerData Player { get; private set; }
    public int Score { get; set; } = 0;

    public GameObject AnounceTextObject
    {
        get { return _anounceTextObject; }
    }

    [Inject]
    public void Construct(EnemySpawner enemySpawner,PlayerData player)
    {
        EnemySpawner = enemySpawner;
        Player = player;
    }
}
