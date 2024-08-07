using System.Collections.Generic;
using UnityEngine;

public interface IConvertableCSVData
{
    Object Convert(List<Dictionary<string, object>> data);
}