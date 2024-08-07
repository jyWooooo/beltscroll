using UnityEngine;

public class MonsterSkeleton : MonsterBase
{
    public override MonsterStatus LoadData()
    {
        return GameManager.DataManager.GetData<DB_MonsterData>("MonsterData").Get("Skeleton");
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + 2f * Time.fixedDeltaTime * Vector3.left);
    }
}