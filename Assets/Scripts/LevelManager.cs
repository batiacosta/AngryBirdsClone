using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;

using ScriptableObjects;

using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private InGameDataSO inGameDataSo;
    [SerializeField] private HearthManagerUI hearthManagerUI;
    [SerializeField] private ScoreManagerUI scoreManagerUI;
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private UIManager uiManager;

    private enum GameState
    {
        Running,
        Finished
    }

    private GameState State
    {
        get => _state;
        set
        {
            _state = value; 
            SetState();
        }
    }
    

    private GameState _state;
    private int _hearts;
    private int _enemies;
    private int _remainingEnemies;
    private int _score;
    private int _birds;
    private void Awake()
    {
        inGameDataSo.InitializeInGameData();
    }

    private void Start()
    {
        _hearts = inGameDataSo.GetHearts();
        UpdateHeart(_hearts);
        inGameDataSo.OnEnemyQuantityChanged += UpdateScore;
        inGameDataSo.OnHeartsChanged += UpdateHearts;
        inGameDataSo.OnBirdQuantityChange += UpdateAvailableBirds;
        SetEnemies();
        if (_birds == 0)
        {
            State = GameState.Finished;
        }
    }

    private void UpdateAvailableBirds(object sender, List<CharacterSOLibrarySO.BirdData> e)
    {
        _birds = 0;
        for (int i = 0; i < e.Count; i++)
        {
            _birds += e[i].Quantity;
        }

        if (_birds == 0)
        {
            State = GameState.Finished;
        }
    }

    private void OnDestroy()
    {
        inGameDataSo.OnEnemyQuantityChanged -= UpdateScore;
        inGameDataSo.OnHeartsChanged -= UpdateHearts;
        inGameDataSo.OnBirdQuantityChange -= UpdateAvailableBirds;
    }

    private void SetEnemies()
    {
        _enemies = enemiesManager.GetEnemies();
        inGameDataSo.SetRemainingEnemies(_enemies);
    }

    private void UpdateHearts(object sender, int e)
    {
        UpdateHeart(e);
    }

    private void UpdateHeart(int hearts)
    {
        _hearts = hearts;
        hearthManagerUI.SetHearts(hearts);
        if (hearts == 0)
        {
            uiManager.DisableHeartButton();
        }
        
    }

    private void UpdateScore(object sender, int e)
    {
        _remainingEnemies = e;
        _score = (int)((_enemies - e) * 10f);
        scoreManagerUI.UpdateScore(_score);
        if (_remainingEnemies == 0)
        {
            State = GameState.Finished;
        }
    }
    
    private void SetState()
    {
        switch (State)
        {
            case GameState.Running:
                SetRunningLevel();
                break;
            case GameState.Finished:
                SetFinishLevel();
                break;
            default: break;
        }
    }

    private void SetRunningLevel()
    {
        uiManager.HideFinalPanels();
    }

    private void SetFinishLevel()
    {
        if (_remainingEnemies == 0)
        {
            uiManager.ShowWinPanel();
            return;
        }

        if (_birds == 0)
        {
            uiManager.ShowLosePanel();
        }
    }

    public void UseHeart()
    {
        inGameDataSo.SetHearts(_hearts - 1);
        inGameDataSo.RefillBirds();
        State = GameState.Running;
    }
}
