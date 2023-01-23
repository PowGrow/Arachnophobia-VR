using UnityEngine;
using Zenject;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    public Transform Transform { get; private set; }
    public bool IsAlive { get; private set; } = true;
    public float Health { get; private set; }

    private void Awake()
    {
        Transform = transform;
        Health = _maxHealth;
    }

    public void TakeDamage(float value)
    {
        Health -= value;
        if(Health <= 0)
        {
            Health = 0;
            IsAlive = false;
            Debug.Log("Player are dead!");
        }
    }
}
