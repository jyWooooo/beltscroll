using Firebase.Database;
using System;
using System.Collections;
using UnityEngine;

public class FirebaseDBManager
{
    private DatabaseReference _dbRef;
    private bool _isConnected = false;

    public void Initialize()
    {
        _dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void TryRead(Action<int> completed = null, Action faulted = null)
    {
        GameManager.Instance.StartCoroutine(CheckConnect(completed, faulted));
    }

    private IEnumerator CheckConnect(Action<int> completed = null, Action faulted = null)
    {
        float timeOut = 10f;
        float startTime = Time.time;
        Ping ping = new("8.8.8.8");

        while (true)
        {
            if (ping.isDone)
            {
                Debug.Log("Network Connect : TRUE");
                _isConnected = true;
                Read(completed, faulted);
                yield break;
            }
            if (Time.time > startTime + timeOut)
            {
                Debug.Log("Network Connect : FALSE");
                faulted?.Invoke();
                yield break;
            }

            yield return null;
        }
    }

    private void Read(Action<int> completed = null, Action faulted = null)
    {
        if (!_isConnected)
            return;

        string id = GameManager.DataManager.UserID.ToString();
        _dbRef.Child(id).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogWarning(task.Exception.ToString());
                faulted?.Invoke();
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Value == null || !int.TryParse(snapshot.Value.ToString(), out int res))
                {
                    faulted?.Invoke();
                    return;
                }

                completed?.Invoke(res);
            }
        });
    }

    public void Write()
    {
        if (!_isConnected)
            return;

        string id = GameManager.DataManager.UserID.ToString();
        _dbRef.Child(id).SetValueAsync(GameManager.StageManager.StageCnt);
    }
}