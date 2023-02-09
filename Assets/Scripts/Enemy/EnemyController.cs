using UnityEngine;
using UnityEngine.AI;
using Zenject;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.Core.Player;
using System;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : HVRDamageHandlerBase
{
    [Header("Settings")]
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
    [SerializeField]
    private ParticleSystem enemyDamageParticle;

    private IAnimationDataProvider _enemyAnimationDataProvider;
    private ISoundProvider _enemySoundProvider;


    private NavMeshAgent _agent;
    private PlayerData _player;
    private float _attackTimer;
    private float _currentHealth;

    private const int DIE = 3;
    private const int ATTACK = 2;
    private const int WALK = 1;
    private const int IDLE = 0;

    public Transform PlayerCamera { get; private set; }

    public event Action<EnemyController> OnEnemyDieEvent;



    [Inject]
    public void Construct(PlayerData player)
    {
        _player = player;
    }

    [ContextMenu("Kill")]
    public void Kill()
    {
        StartCoroutine(Die());
    }

    [ContextMenu("Damage")]
    public void Damage()
    {
        TakeDamage(5);
    }

    private bool CanAttack()
    {
        var distance = Vector3.Distance(transform.position, _player.Transform.position);
        if (_attackTimer >= attackDelay && _player.IsAlive &&  distance <= attackDistance)
        {
            _attackTimer = 0;
            return true;
        }
        return false;
    }

    private void MoveToTarget(PlayerData player)
    {
        if (player != null)
        {
            if (_agent.destination != player.Transform.position)
            {
                _agent.SetDestination(player.Transform.position);
            }
        }
    }

    private bool ThereIsNoTarget()
    {
        if (_player != null)
            return false;
        return true;
    }

    public override void TakeDamage(float value)
    {
        enemyDamageParticle.Play();
        _currentHealth -= value;
        enemyUi.UpdateUI(_currentHealth, health);
        if (health <= 0)
            StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        _agent.enabled = false;
        if (_player.IsAlive)
            DoAction(DIE);
        yield return new WaitForSeconds(3);
        OnEnemyDieEvent?.Invoke(this);
        Destroy(gameObject);
    }

    private void DoAction(int actionId)
    {
        if (_enemyAnimationDataProvider != null)
        {
            if (actionId == ATTACK)
                _enemyAnimationDataProvider.IsAttacking = true;
            else
                _enemyAnimationDataProvider.IsAttacking = false;
            if(actionId == DIE)
            {
                _enemyAnimationDataProvider.IsAttacking = false;
                _enemyAnimationDataProvider.IsAlive = false;
            }
            if (_enemySoundProvider.CurrentState != (SoundProviderStatesEnum)actionId)
                _enemySoundProvider.PlaySound(actionId);
        }
    }

    public void Mutate(float damageDelta = 0, float healthDelta = 0)
    {
        damage += damageDelta;
        health += healthDelta;
        _currentHealth = health;
    }

    private void Awake()
    {
        _enemyAnimationDataProvider = GetComponent<IAnimationDataProvider>();
        _enemySoundProvider = GetComponent<ISoundProvider>();
        _agent = GetComponent<NavMeshAgent>();
        PlayerCamera = _player.GetComponent<HVRPlayerController>().Camera;
    }

    private void Start()
    {
        _currentHealth = health;
        MoveToTarget(_player);
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (ThereIsNoTarget())
            return;

        if (CanAttack())
        {
            DoAction(ATTACK);
            _player.TakeDamage(damage);
            return;
        }
        else
        {
            if (_enemyAnimationDataProvider.CurrentState == IDLE)
                DoAction(IDLE);
            else
                if (!_agent.hasPath)
                    DoAction(IDLE);
                DoAction(WALK);
        }
    }
}
