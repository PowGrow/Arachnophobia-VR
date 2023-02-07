using HurricaneVR.Framework.Core.Player;
using UnityEngine;
using Zenject;

public class PauseUI : MonoBehaviour
{
    private Canvas _canvas;
    private PlayerData _player;
    [Inject]
    public void Construct(PlayerData player)
    {
        _player = player;
    }
    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = _player.GetComponent<HVRPlayerController>().Camera.GetComponent<Camera>();
    }
}
