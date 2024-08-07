using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private ResourceManager _resourceManager;
    private DataManager _dataManager;
    private UIManager _uiManager;
    private SceneManager _sceneManager;

    public static ResourceManager ResourceManager => Instance._resourceManager;
    public static DataManager DataManager => Instance._dataManager;
    public static UIManager UIManager => Instance._uiManager;
    public static SceneManager SceneManager => Instance._sceneManager;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;
        DontDestroyOnLoad(gameObject);
        CreateManagerInstance();
        _dataManager.OnDBLoaded += InitializeManagers;
        _dataManager.DBLoad();
        return true;
    }

    private void CreateManagerInstance()
    {
        _resourceManager = new();
        _dataManager = new();
        _uiManager = new();
        _sceneManager = new();
    }

    private void InitializeManagers()
    {

    }
}