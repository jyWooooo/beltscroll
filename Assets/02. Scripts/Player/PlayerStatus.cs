using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    [field: SerializeField] public float AttackDamage { get; private set; } = 100f;
    [field: SerializeField] public float AttackInterval { get; private set; } = 1f;
    [field: SerializeField] public float AttackRange { get; private set; } = 5f;
}