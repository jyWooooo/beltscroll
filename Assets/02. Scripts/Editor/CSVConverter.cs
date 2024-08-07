using UnityEngine;
using UnityEditor;
using System;

public static class CSVConverter
{
    [MenuItem("Assets/Convert CSV/MonsterData.csv")]
    public static void ConvertMonsterData()
    {
        string loadPath = @"Assets/CSV Data/MonsterData.csv";
        string savePath = @"Assets/04. Database/DB_MonsterData.asset";

        var csv = AssetDatabase.LoadAssetAtPath<TextAsset>(loadPath);
        var data = CSVReader.Read(csv);

        MonsterStatus[] statuses = new MonsterStatus[data.Count];
        for (int i = 0; i < data.Count; i++)
        {
            statuses[i] = new MonsterStatus(
                (string)data[i]["Name"],
                Enum.Parse<MonsterStatus.GradeType>(data[i]["Grade"].ToString()),
                (float)data[i]["Speed"],
                (int)data[i]["Health"]);
        }

        var so = ScriptableObject.CreateInstance<DB_MonsterData>();
        so.Set(statuses);
        AssetDatabase.CreateAsset(so, savePath);
        EditorGUIUtility.PingObject(so);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"CSV conversion was successful. The asset file has been saved to {savePath}.");
    }
}
