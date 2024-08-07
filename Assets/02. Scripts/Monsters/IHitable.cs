using System;
using UnityEngine;

public interface IHitable
{
    event Action OnHit;
    void Hit(float damage);
    Vector3 GetPosition();
    bool IsUnityNull();
}