using UnityEngine;

[CreateAssetMenu(fileName = "DB_NewWorldSettings", menuName = "Scriptable Object/WorldSettings")]
public class DB_WorldSettings : ScriptableObject
{
    [field: SerializeField] public Vector3 PlayerSpawnPosition { get; private set; }
    [field: SerializeField] public Vector3 MonsterSpawnPosition { get; private set; }
    [field: SerializeField] public float MoveNextStageAnimateTime { get; private set; }
}