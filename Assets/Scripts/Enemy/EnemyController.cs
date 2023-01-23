using UnityEngine;
using UnityEngine.AI;
using Zenject;
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _attackDistance;

    private NavMeshAgent _agent;
    private PlayerData _player;

    [Inject]
    public void Construct(PlayerData player)
    {
        _player = player;
    }

    private bool CanAttack()
    {
        if (Vector3.Distance(transform.position, _player.Transform.position) <= _attackDistance) return true;
        else
            return false;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _agent.destination = _player.Transform.position;
    }

    private void FixedUpdate()
    {
        if (CanAttack())
        {
            _agent.isStopped = true;
            if (_player.IsAlive)
                _player.TakeDamage(_damage);
        }
        else
        {
            _agent.isStopped = false;
        }
    }

}
