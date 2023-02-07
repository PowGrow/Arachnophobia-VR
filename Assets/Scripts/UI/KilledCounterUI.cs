using TMPro;
using UnityEngine;

public class KilledCounterUI : MonoBehaviour
{
    [SerializeField]
    private GameUI gameUI;
    [SerializeField]
    private TextMeshProUGUI killedCounterText;

    private void UpdateUI()
    {
        gameUI.Score++;
        killedCounterText.text = gameUI.Score.ToString(); 
    }

    private void OnEnable()
    {
        gameUI.EnemySpawner.OnEnemyKilledEvent += UpdateUI;
    }

    private void OnDisable()
    {
        gameUI.EnemySpawner.OnEnemyKilledEvent -= UpdateUI;
    }
}
