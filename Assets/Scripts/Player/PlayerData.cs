using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    public Transform Transform { get; private set; }
    public bool IsAlive { get; private set; } = true;
    public float Health { get; private set; }

    private const string START_SCENE = "StartScene";

    private void Awake()
    {
        Transform = transform;
        Health = maxHealth;
    }

    public void TakeDamage(float value)
    {
        Health -= value;
        if(Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Health = 0;
        IsAlive = false;
        SceneManager.LoadScene(START_SCENE);
    }
}
