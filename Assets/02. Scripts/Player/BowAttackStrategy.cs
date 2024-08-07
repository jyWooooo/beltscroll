using UnityEngine;

public class BowAttackStrategy : IAttackStrategy
{
    private PlayerCharacter _player;
    private IHitable _target;
    private GameObject _arrowPrefab;

    public event System.Action OnAttacked;

    public BowAttackStrategy(PlayerCharacter player)
    {
        _player = player;
        _arrowPrefab = GameManager.ResourceManager.GetCache<GameObject>("ArrowProjectile.prefab");
    }

    public bool SearchTarget()
    {
        var col = Physics2D.OverlapCircle(_player.transform.position, _player.Status.AttackRange, 1 << 7);
        if (col == null)
        {
            _target = null;
            return false;
        }
        _target = col.GetComponent<IHitable>();
        return true;
    }

    public void Attack()
    {
        Object.Instantiate(_arrowPrefab, _player.AttackTrigger.transform.position, Quaternion.identity);
        OnAttacked?.Invoke();
    }
}