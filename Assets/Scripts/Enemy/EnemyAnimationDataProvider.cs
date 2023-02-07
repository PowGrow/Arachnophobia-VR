using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationDataProvider : MonoBehaviour, IAnimationDataProvider
{
    private NavMeshAgent _agent;
    private Animator _animator;

    public bool IsAlive { get; set; }
    public bool IsAttacking { get; set; }

    public SoundProviderStatesEnum CurrentState { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        IsAlive = true;
        IsAttacking = false;
    }

    private void FixedUpdate()
    {
        if (!IsAlive)
        {
            CurrentState = SoundProviderStatesEnum.Die;
            _animator.SetBool("IsAlive", false);
            return;
        }

        if (IsAttacking)
        {
            CurrentState = SoundProviderStatesEnum.Attack;
            _animator.SetBool("IsAttacking", true);
        }
        else
        {
            _animator.SetBool("IsAttacking", false);
        }

        if (Mathf.Abs(_agent.velocity.x) <= 0.08 && Mathf.Abs(_agent.velocity.y) <= 0.08)
        {
            CurrentState = SoundProviderStatesEnum.Idle;
            _animator.SetBool("IsMoving", false);
        }
        else
        {
            CurrentState = SoundProviderStatesEnum.Walk;
            _animator.SetBool("IsMoving", true);
        }

    }
}
