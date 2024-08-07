using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : IConvertableCSVData
{
    public enum GradeType
    {
        Common,
        Rare,
        Magic,
        Legendary,
        Hero,
    }

    public string Name { get; private set; }
    public GradeType Grade { get; private set; }
    public float Speed { get; private set; }
    public float Health { get; private set; }

    public Object Convert(List<Dictionary<string, object>> data)
    {
        return null;
    }
}