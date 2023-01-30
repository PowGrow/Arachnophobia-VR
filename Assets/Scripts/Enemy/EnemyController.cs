using UnityEngine;
using UnityEngine.AI;
using Zenject;
using HurricaneVR.Framework.Components;

public class EnemyController : HVRDamageHandlerBase
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private float health;
    [SerializeField]
    private EnemyUI enemyUi;


    private NavMeshAgent _agent;
    private PlayerData _player;
    private float _attackTimer;
    private float _currentHealth;

    [Inject]
    public void Construct(PlayerData player)
    {
        _player = player;
    }

    private bool CanAttack()
    {
        if (_attackTimer >= attackDelay && _player.IsAlive)
        {
            if (Vector3.Distance(transform.position, _player.Transform.position) <= attackDistance)
            {
                _attackTimer = 0;
                return true;
            }
        }
        return false;
    }

    private void MoveToTarget(PlayerData _player)
    {
        if (_agent.destination != _player.Transform.position)
        {
            _agent.SetDestination(_player.Transform.position);
        }
    }

    private bool ThereIsATarget()
    {
        if (_player != null)
            return true;
        return false;
    }

    public override void TakeDamage(float value)
    {
        _currentHealth -= value;
        enemyUi.UpdateUI(_currentHealth, health);
        if (health <= 0)
            Destroy(this.gameObject);
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _currentHealth = health;
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (ThereIsATarget())
        {
            MoveToTarget(_player);
            if (CanAttack())
                _player.TakeDamage(damage);
        }
    }

}
