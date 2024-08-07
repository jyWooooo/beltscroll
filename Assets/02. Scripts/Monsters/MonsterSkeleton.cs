using UnityEngine;

public class MonsterSkeleton : MonsterBase
{
    public override MonsterStatus LoadData()
    {
        return GameManager.DataManager.GetData<DB_MonsterData>("MonsterData").Get("Skeleton");
    }
}