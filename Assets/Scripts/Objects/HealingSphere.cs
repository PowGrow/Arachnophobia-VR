using UnityEngine;
using Zenject;

public class HealingSphere : MonoBehaviour
{
    [SerializeField]
    private float healingAmount = 15f;
    [SerializeField]
    private float gravityTriggervalue;
    private PlayerData _player;
    private Rigidbody _rigidbody;
    private Transform _transform;
    [Inject]
    public void Construct(PlayerData player)
    {
        _player = player;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    public void Activate()
    {
        _player.TakeHealing(healingAmount);
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (_transform.position.y < gravityTriggervalue)
        {
            _rigidbody.useGravity = false;
            _rigidbody.AddForce(new Vector3(0, gravityTriggervalue, 0));
        }
        else
        {
            _rigidbody.useGravity = true;
        }
            
    }
}
