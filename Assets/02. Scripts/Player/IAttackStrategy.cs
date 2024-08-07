using System;

public interface IAttackStrategy
{
    event Action OnAttacked;
    bool TryAttack();
}