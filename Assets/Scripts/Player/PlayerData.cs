using System;
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

    private AudioSource _audioSource;
    private const string START_SCENE = "StartScene";

    public event Action<float, float> OnPlayerGetHurtEvent;
    private void Awake()
    {
        Transform = transform;
        Health = maxHealth;
        _audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float value)
    {

        _audioSource.Play();
        Health -= value;
        OnPlayerGetHurtEvent?.Invoke(Health,maxHealth);
        if (Health <= 0)
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
