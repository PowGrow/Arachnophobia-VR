using HurricaneVR.Framework.Core;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField]
    private int secondPistolScore;
    [SerializeField]
    private int shotgunScore;
    [SerializeField]
    private int smgScore;
    [SerializeField]
    private GameObject pistolPrefab;
    [SerializeField]
    private GameObject shotgunPrefab;
    [SerializeField]
    private GameObject smgPrefab;
    [SerializeField]
    private Transform spawnPoint;
    private GameUI _gameUI;
    private EnemySpawner _enemySpawner;
    private DiContainer _container;
    private TextMeshProUGUI _anounceTextComponent;

    private const string PISTOL_TEXT = "FIGHT WITH YOUR FEARS DUDE!\r\nSECOND PISTOL WILL HELP U WITH THIS!";
    private const string SHOTGUN_TEXT = "WE NEED MORE GUNPOWER!\r\nSHOTGUN THEM ALL!";
    private const string SMG_TEXT = "SO SAD U CANT IMAGINE MACHINEGUN...\r\nBUT YOU CAN SMG..KILL THEM ALL!";
    [Inject]
    public void Construct(DiContainer container,GameUI gameUI,EnemySpawner enemySpawner)
    {
        _container = container;
        _gameUI = gameUI;
        _enemySpawner = enemySpawner;
    }

    private void TryToSpawnNewWeapon()
    {
        if(_gameUI.Score == secondPistolScore)
            SpawnNewWeapon(pistolPrefab, PISTOL_TEXT);
        if (_gameUI.Score == shotgunScore)
            SpawnNewWeapon(shotgunPrefab, SHOTGUN_TEXT);
        if (_gameUI.Score == smgScore)
            SpawnNewWeapon(smgPrefab, SMG_TEXT);
    }

    private void SpawnNewWeapon(GameObject weaponToSpawn, string textToShow)
    {
        _container.InstantiatePrefab(weaponToSpawn, spawnPoint.position, Quaternion.identity, null);
        StartCoroutine(ShowText(textToShow));
    }

    private IEnumerator ShowText(string textToShow)
    {
        _anounceTextComponent.text = textToShow;
        _anounceTextComponent.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        _anounceTextComponent.gameObject.SetActive(false);
    }

    private void Awake()
    {
        _anounceTextComponent = _gameUI.AnounceTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _enemySpawner.OnEnemyKilledEvent += TryToSpawnNewWeapon;
    }

    private void OnDisable()
    {
        _enemySpawner.OnEnemyKilledEvent -= TryToSpawnNewWeapon;
    }
}
