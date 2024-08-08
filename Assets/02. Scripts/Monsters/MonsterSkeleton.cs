using UnityEngine;

public class MonsterSkeleton : MonsterBase
{
    public override MonsterStatus LoadData()
    {
        return GameManager.MonsterManager.MonsterData.GetMonsterData("Skeleton");
    }
}