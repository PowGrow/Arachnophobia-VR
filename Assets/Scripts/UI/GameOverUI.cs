using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private GameUI gameUi;
    [SerializeField]
    private GameObject gameOverLabel;
    [SerializeField]
    private TextMeshProUGUI gameOverScoreLable;
    private void UpdateUI()
    {
        gameOverScoreLable.text = gameUi.Score.ToString();
        gameOverLabel.SetActive(true);
    }

    private void OnEnable()
    {
        gameUi.Player.OnPlayerDieEvent += UpdateUI;
    }

    private void OnDisable()
    {
        gameUi.Player.OnPlayerDieEvent -= UpdateUI;
    }
}
