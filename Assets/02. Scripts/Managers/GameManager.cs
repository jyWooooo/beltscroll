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
    private MonsterManager _monsterManager;
    private StageManager _stageManager;
    private FirebaseDBManager _firebaseDBManager;

    public static ResourceManager ResourceManager => Instance._resourceManager;
    public static DataManager DataManager => Instance._dataManager;
    public static UIManager UIManager => Instance._uiManager;
    public static SceneManager SceneManager => Instance._sceneManager;
    public static MonsterManager MonsterManager => Instance._monsterManager;
    public static StageManager StageManager => Instance._stageManager;
    public static FirebaseDBManager FirebaseDBManager => Instance._firebaseDBManager;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;
        DontDestroyOnLoad(gameObject);
        CreateManagerInstance();
        _dataManager.Load(InitializeManagers);
        return true;
    }

    private void CreateManagerInstance()
    {
        _resourceManager = new();
        _dataManager = new();
        _uiManager = new();
        _sceneManager = new();
        _monsterManager = new();
        _stageManager = new();
        _firebaseDBManager = new();
    }

    private void InitializeManagers()
    {
        _firebaseDBManager.Initialize();
        _monsterManager.Initialize();
        _stageManager.Initialize();
    }
}