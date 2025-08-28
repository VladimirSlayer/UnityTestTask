using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class HealthController : MonoBehaviour
{
    [SerializeField] private float _health = 100f;

    public float Health => _health;

    public void Start()
    {
        UIManager.Instance.RefreshHealth(Health);
    }

    public void ApplyDamage(float amount)
    {
        _health -= amount;
        if (_health <= 0f)
        {
            UIManager.Instance.ShowGameOverScreen();
        }
    }
}
