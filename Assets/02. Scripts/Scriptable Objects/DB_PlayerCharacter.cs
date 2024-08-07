using UnityEngine;

[CreateAssetMenu(fileName = "DB_NewPlayerCharacter", menuName = "Scriptable Object/PlayerCharacter")]
public class DB_PlayerCharacter : ScriptableObject
{
    public enum AttackStrategyType
    {
        Bow,
    }

    [field: SerializeField] public string CharacterName { get; private set; }
    [field: SerializeField] public PlayerStatus PlayerStatus { get; private set; }
    [field: SerializeField] public RuntimeAnimatorController Controller { get; private set; }
    [field: SerializeField] public AttackStrategyType AttackStrategy { get; private set; }

    public IAttackStrategy CreateAttackStrategyInstance(PlayerCharacter player)
    {
        return AttackStrategy switch
        {
            AttackStrategyType.Bow => new BowAttackStrategy(player),
            _ => null,
        };
    }
}