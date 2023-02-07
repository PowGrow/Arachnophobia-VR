using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerHpUI : MonoBehaviour
{
    [SerializeField]
    private GameUI gameUi;
    private Image _hpImage;


    private const float CAP = 100; //100 %
    private void UpdateUi(float currentHealth,float maxHealth)
    {
        var healthDelta = maxHealth / CAP;
        var value = CAP -  currentHealth / healthDelta;
        Color newColor = _hpImage.color;
        newColor.a = value / CAP;
        _hpImage.color = newColor;
        
    }

    private void Awake()
    {
        _hpImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        gameUi.Player.OnPlayerGetHurtEvent += UpdateUi;
    }

    private void OnDisable()
    {
        gameUi.Player.OnPlayerGetHurtEvent -= UpdateUi;
    }
}
