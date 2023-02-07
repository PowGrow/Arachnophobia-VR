using System;
using System.Collections;
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
    private const float DIE_DELAY = 10f;

    public event Action<float, float> OnPlayerGetHurtEvent;
    public event Action OnPlayerDieEvent;

    [ContextMenu("Suicide")]
    public void Suicide()
    {
        StartCoroutine(Die(DIE_DELAY));
    }

    public void TakeDamage(float value)
    {
        _audioSource.Play();
        Health -= value;
        OnPlayerGetHurtEvent?.Invoke(Health, maxHealth);
        if (Health <= 0)
        {
            StartCoroutine(Die(DIE_DELAY));
        }
    }
    private IEnumerator Die(float delay)
    {
        Health = 0;
        IsAlive = false;
        OnPlayerDieEvent?.Invoke();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(START_SCENE);
    }

    private void Awake()
    {
        Transform = transform;
        Health = maxHealth;
        _audioSource = GetComponent<AudioSource>();
    }

}
