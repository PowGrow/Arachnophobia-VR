using UnityEngine;
using Zenject;

public class GameOverSoundProvider : MonoBehaviour
{
    [SerializeField]
    private AudioClip gameOverSound;
    private AudioSource _audioSource;
    private PlayerData _player;
    [Inject]
    public void Construct(PlayerData player)
    {
        _player = player;
    }

    private void UpdateSound()
    {
        _audioSource.loop = false;
        _audioSource.Stop();
        _audioSource.clip = gameOverSound;
        _audioSource.Play();
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        _player.OnPlayerDieEvent += UpdateSound;
    }

    private void OnDisable()
    {
        _player.OnPlayerDieEvent -= UpdateSound;
    }
}
