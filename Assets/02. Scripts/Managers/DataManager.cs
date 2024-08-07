using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public bool IsDone { get; private set; } = false;

    private Dictionary<string, ScriptableObject> _db = new();

    public event Action OnDBLoaded;

    public void DBLoad(Action<string, int, int> callback = null)
    {
        callback += LoadCompleteCallback;
        GameManager.ResourceManager.LoadAllAsync<UnityEngine.Object>("DB", callback);
    }

    public T GetData<T>(string key) where T : ScriptableObject
    {
        if (!_db.TryGetValue($"DB_{key}.data", out var data))
            return null;
        return data as T;
    }

    private void LoadCompleteCallback(string key, int cnt, int total) 
    {
        _db.Add(key, GameManager.ResourceManager.GetCache<ScriptableObject>(key));

        if (cnt == total)
        {
            IsDone = true;
            OnDBLoaded?.Invoke();
        }
    }
}