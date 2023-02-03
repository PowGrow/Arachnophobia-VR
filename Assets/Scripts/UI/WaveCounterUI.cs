using TMPro;
using UnityEngine;

public class WaveCounterUI : MonoBehaviour
{
    [SerializeField]
    private GameUI gameUI;
    [SerializeField]
    private TextMeshProUGUI waveCounterText;

    private void UpdateUI(int waveIndex)
    {
        waveCounterText.text = waveIndex.ToString();
    }

    private void OnEnable()
    {
        gameUI.EnemySpawner.OnWaveChangingEvent += UpdateUI;
    }

    private void OnDisable()
    {
        gameUI.EnemySpawner.OnWaveChangingEvent -= UpdateUI;
    }
}
