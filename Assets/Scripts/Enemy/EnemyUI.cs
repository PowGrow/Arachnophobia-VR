using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]
    private Image healthUi;
    private Transform _playerCameraTransform;
    private PlayerData _player;


    public void UpdateUI(float currentHealth, float maxHealth)
    {
        healthUi.fillAmount = currentHealth / maxHealth;
    }
    private void Start()
    {
        _playerCameraTransform = transform.parent.GetComponent<EnemyController>().PlayerCamera;
    }

    private void Update()
    {
        transform.LookAt(_playerCameraTransform);
    }
}
