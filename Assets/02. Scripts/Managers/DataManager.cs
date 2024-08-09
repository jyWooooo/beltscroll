using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class DataManager
{
    private Dictionary<string, ScriptableObject> _db = new();
    private DatabaseReference _firebaseDBRef;
    private UserGUIDManager _userGUIDManager = new();

    public bool IsDone { get; private set; } = false;
    public Guid UserID => _userGUIDManager.GetUserID();

    public event Action OnDBLoaded;

    public void DBLoad(Action<string, int, int> callback = null)
    {
        _firebaseDBRef = FirebaseDatabase.DefaultInstance.RootReference;

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