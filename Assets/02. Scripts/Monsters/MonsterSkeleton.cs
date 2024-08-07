using UnityEngine;

public class MonsterSkeleton : MonsterBase
{
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + 2f * Time.fixedDeltaTime * Vector3.left);
    }
}