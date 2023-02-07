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

    [SerializeField]
    private AudioSource _loopAudioSource;
    [SerializeField]
    private AudioSource _noRepeatAudioSource;

    public SoundProviderStatesEnum CurrentState {get; private set;}


    public void PlaySound(int state)
    {
        switch (state)
        {
            case 0:
                _loopAudioSource.clip = idleSound;
                _loopAudioSource.Play();
                CurrentState = SoundProviderStatesEnum.Idle;
                break;

            case 1:
                _loopAudioSource.clip = walkSound;
                _loopAudioSource.Play();
                CurrentState = SoundProviderStatesEnum.Walk;
                break;

            case 2:
                _noRepeatAudioSource.clip = attackSound;
                _noRepeatAudioSource.Play();
                CurrentState = SoundProviderStatesEnum.Attack;
                break;

            case 3:
                _loopAudioSource.Stop();
                _noRepeatAudioSource.clip = dieSound;
                _noRepeatAudioSource.Play();
                CurrentState = SoundProviderStatesEnum.Die;
                break;
        }
    }
}
