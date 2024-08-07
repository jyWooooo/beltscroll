using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    private bool _isInitialized = false;

    public virtual void Start()
    {
        GameManager.SceneManager.SetScene(this);
    }

    public virtual bool Initialize()
    {
        if (_isInitialized) return false;
        _isInitialized = true;
        return true;
    }

    public abstract void LoadScene();
    public abstract void UnloadScene();
    public virtual void OnDestroy() { }
    public virtual void OnLoadFail() { }
    public virtual void OnLoadSuccess() { }
}