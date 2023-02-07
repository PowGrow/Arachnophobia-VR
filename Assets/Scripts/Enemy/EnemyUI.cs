using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]
    private Image healthUi;
    private Transform _playerCameraTransform;


    [ContextMenu("TestUI")]
    public void TestUI()
    {
        healthUi.fillAmount = 50f / 100f;
        Debug.Log(healthUi.fillAmount);
    }
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
