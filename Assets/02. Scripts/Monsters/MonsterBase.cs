using System;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour, IHitable
{
    private float _currentHP;

    public MonsterStatus Status { get; private set; }

    public event Action OnHit;

    public void Initialize()
    {
        _currentHP = Status.Health;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void Hit(float damage)
    {
        Debug.Log(_currentHP);
        OnHit?.Invoke();
        _currentHP -= damage;
        if (_currentHP <= 0f)
            Die();
    }

    public bool IsUnityNull()
    {
        return !this;
    }
}