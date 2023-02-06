public interface IAnimationDataProvider
{
    bool IsAlive { get; set; }
    bool IsAttacking { get; set; }
    SoundProviderStatesEnum CurrentState { get; }
}