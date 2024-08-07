using System;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour, IHitable
{
    private float _currentHP;
    protected Rigidbody2D _rigidbody;

    public MonsterStatus Status { get; private set; }

    public event Action OnHit;

    private void Awake()
    {
        Debug.Log(GetType().Name + "Awake");
        _rigidbody = GetComponent<Rigidbody2D>();
        Status = LoadData();
    }

    public abstract MonsterStatus LoadData();

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