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

    public bool TryAttack()
    {
        SearchTarget();
        if (_target == null)
            return false;
        Shot();
        return true;
    }

    private void SearchTarget() 
    {
        var col = Physics2D.OverlapCircle(_player.transform.position, _player.Status.AttackRange);
        if (col == null) 
            return;
        _target = col.GetComponent<IHitable>();
    }

    public void Shot()
    {
        Object.Instantiate(_arrowPrefab, _player.transform.position, Quaternion.identity);
    }
}