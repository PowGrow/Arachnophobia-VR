using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemySoundProvider : MonoBehaviour, ISoundProvider
{
    [SerializeField]
    private AudioClip walkSound;
    [SerializeField]
    private AudioClip attackSound;
    [SerializeField]
    private AudioClip idleSound;
    [SerializeField]
    private AudioClip dieSound;

    private AudioSource _audioSource;

    public SoundProviderStatesEnum CurrentState {get; private set;}

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int state)
    {
        _audioSource.Stop();
        switch (state)
        {
            case 0:
                _audioSource.loop = true;
                _audioSource.clip = idleSound;
                _audioSource.Play();
                CurrentState = SoundProviderStatesEnum.Idle;
                break;

            case 1:
                _audioSource.loop = true;
                _audioSource.clip = walkSound;
                _audioSource.Play();
                CurrentState = SoundProviderStatesEnum.Walk;
                break;

            case 2:
                _audioSource.loop = false;
                _audioSource.clip = attackSound;
                _audioSource.Play();
                CurrentState = SoundProviderStatesEnum.Attack;
                break;

            case 3:
                _audioSource.loop = false;
                _audioSource.clip = dieSound;
                _audioSource.Play();
                CurrentState = SoundProviderStatesEnum.Die;
                break;
        }
    }
}
