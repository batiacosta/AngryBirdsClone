using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private InGameDataSO inGameDataSo;
    [SerializeField] private HearthManagerUI hearthManagerUI;
    [SerializeField] private ScoreManagerUI scoreManagerUI;
    [SerializeField] private EnemiesManager enemiesManager;


    private int _hearts;
    private int _enemies;
    private int _remainingEnemies;
    private int _score;
    private void Awake()
    {
        inGameDataSo.InitializeInGameData();
    }

    private void Start()
    {
        UpdateHeart(inGameDataSo.GetHearts());
        inGameDataSo.OnEnemyQuantityChanged += UpdateScore;
        inGameDataSo.OnHeartsChanged += UpdateHearts;
        SetEnemies();
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
    }

    private void UpdateScore(object sender, int e)
    {
        _remainingEnemies = e;
        _score = (int)((_enemies - e) * 10f);
        scoreManagerUI.UpdateScore(_score);
    }
}
