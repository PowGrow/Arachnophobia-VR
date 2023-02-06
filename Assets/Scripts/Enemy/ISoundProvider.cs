public interface ISoundProvider
{
    public SoundProviderStatesEnum CurrentState { get; }
    public void PlaySound(int state);
}