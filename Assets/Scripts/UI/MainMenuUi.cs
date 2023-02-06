using HurricaneVR.Framework.Core.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    private PlayerData _player;
    private Canvas _canvas;

    [ContextMenu("StartGame")]

    [Inject]
    public void Construct(PlayerData player)
    {
        _player = player;
    }
    
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        _canvas.worldCamera = _player.GetComponent<HVRPlayerController>().Camera.GetComponent<Camera>();
    }
}
