using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody;
    private float _damage;


    public void Set(float damage)
    {
        _damage = damage;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.right * _speed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var hit = col.GetComponent<IHitable>();
        if (!hit.IsUnityNull())
        {
            hit.Hit(_damage);
            Destroy(gameObject);
        }
    }
}