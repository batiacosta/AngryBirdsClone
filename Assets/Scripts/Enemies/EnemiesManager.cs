
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private List<BaseEnemy> enemies;

    public int GetEnemies()
    {
        return enemies.Count;
    }
}
