using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
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
        if (_lifeTime > 0f)
            _lifeTime -= Time.fixedDeltaTime;
        else
            Destroy(gameObject);

        _rigidbody.MovePosition(transform.position + _speed * Time.fixedDeltaTime * transform.right);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var hit = col.GetComponent<IHitable>();
        if (hit != null || !hit.IsUnityNull())
        {
            hit.Hit(_damage);
            Destroy(gameObject);
        }
    }
}