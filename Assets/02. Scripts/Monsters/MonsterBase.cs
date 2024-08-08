using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonsterBase : MonoBehaviour, IHitable
{
    private float _currentHP;
    private Collider2D _collider;
    private Slider _hpBar;

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public MonsterFSM MonsterFSM { get; private set; }
    public MonsterStatus Status { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    public event Action OnHit;
    public event Action OnDied;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _hpBar = GetComponentInChildren<Slider>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        MonsterFSM = new(this);
        Status = LoadData();
        _currentHP = Status.Health;
        _hpBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        MonsterFSM.Update();
    }

    private void FixedUpdate()
    {
        MonsterFSM.FixedUpdate();
    }

    public abstract MonsterStatus LoadData();

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public virtual void Die()
    {
        _collider.enabled = false;
        OnDied?.Invoke();
    }

    public void Hit(float damage)
    {
        OnHit?.Invoke();
        _currentHP -= damage;
        if (_currentHP <= 0f)
        {
            Die();
            _currentHP = 0f;
        }
        _hpBar.gameObject.SetActive(true);
        _hpBar.value = _currentHP / Status.Health;
    }

    public bool IsUnityNull()
    {
        return !this;
    }
}