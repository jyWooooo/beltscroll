using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterStatus
{
    public enum GradeType
    {
        Common,
        Rare,
        Magic,
        Legendary,
        Hero,
    }

    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public GradeType Grade { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Health { get; private set; }

    public MonsterStatus(string name, GradeType grade, float speed, float health)
    {
        Name = name;
        Grade = grade;
        Speed = speed;
        Health = health;
    }
}